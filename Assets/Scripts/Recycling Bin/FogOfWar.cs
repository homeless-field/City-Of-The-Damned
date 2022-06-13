using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWar : MonoBehaviour
{
    [SerializeField] private int fogResolution, edgeAccuracy;
    [SerializeField] private float viewDistance, fogZPos;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private MeshFilter meshFilter;
    private Mesh mesh;

    List<RayResult> fogPoints = new List<RayResult>();
    Vector3[] meshVertices;
    int[] meshTriangles;

    // A STRUCT REPRESENTING THE RESULTS OF A RAYCAST
    struct RayResult
    {
        public float angle;
        public Vector3 point;
        public bool hitObject;

        public RayResult(float angle, Vector3 point, bool hitObject)
        {
            this.angle = angle;
            this.point = point;
            this.hitObject = hitObject;
        }
    }

    // SEND A RAYCAST OUT AND RETURN THE RAYPOINT
    private RayResult SendRay(float rayAngle, Vector3 rayPos, float rayLength)
    {
        Vector3 rayVector = new Vector3(Mathf.Cos(rayAngle * Mathf.Deg2Rad), Mathf.Sin(rayAngle * Mathf.Deg2Rad), 0);
        Ray ray = new Ray(rayPos, rayVector);

        if (Physics.Raycast(ray, out RaycastHit hit, rayLength, layerMask))
            return new RayResult(rayAngle, hit.point - transform.position, true);
        else
            return new RayResult(rayAngle, rayVector * rayLength, false);
    }

    private void Start()
    {
        mesh = new Mesh();
        meshFilter.mesh = mesh;
        mesh.MarkDynamic();
    }

    private void LateUpdate()
    {
        // SET STUFF UP
        fogPoints.Clear();
        float fogAngles = 360 / fogResolution;
        transform.SetPositionAndRotation(transform.position, Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z));

        // SEND RAYCASTS IN A RADIAL PATTERN
        for (int i = 0; i < fogResolution; i++)
        {
            RayResult raycast = SendRay(transform.eulerAngles.y + (fogAngles * i), transform.position, viewDistance);
            fogPoints.Add(raycast);

            // CREATE EXTRA POINTS AT OBJECT EDGES, TO PREVENT JITTER
            /**if (i > 1 && (raycast.hitObject != fogPoints[i - 1].hitObject)) // We don't run this the first few times because those points use separate logic
            {
                Debug.Log("New status");

                // SET UP INITIAL POINTS
                RayResult testRay;
                RayResult prevRaycast = fogPoints[i - 1];
                RayResult minRay = new RayResult(prevRaycast.angle, Vector3.zero, false);
                RayResult maxRay = new RayResult(raycast.angle, Vector3.zero, false);

                // ESTIMATE THE EDGE POSITION, USING A BINARY TREE
                for (int j = 0; j < edgeAccuracy; j++)
                {
                    Debug.Log("Estimating");
                    float angle = (minRay.angle + maxRay.angle) / 2;
                    testRay = SendRay(transform.eulerAngles.y + angle, transform.position, viewDistance);

                    if (testRay.hitObject == maxRay.hitObject)
                        minRay = testRay;
                    else
                        maxRay = testRay;
                }

                Debug.Log("Done estimating");

                // ADD A POINT ON EITHER END OF THE EDGE
                if (minRay.point != Vector3.zero)
                {
                    fogPoints.Add(minRay);
                }

                if (maxRay.point != Vector3.zero)
                {
                    fogPoints.Add(maxRay);
                }
            }**/
        }

        meshVertices = new Vector3[fogPoints.Count + 1]; // We add 1 to fogpoints to account for the central vertex
        meshVertices[0] = new Vector3(0, 0, fogZPos);
        meshTriangles = new int[fogPoints.Count * 3]; // We multiply fogresolution by 3 to account for the 3 vertices per triangle

        // PREPARE MESH ARRAYS
        for (int i = 0; i < fogPoints.Count; i++)
        {
            RayResult fogPoint = fogPoints[i];

            meshVertices[i + 1] = new Vector3(fogPoint.point.x, fogPoint.point.y, fogZPos); // We add 1 to i because meshVertices[0] is already defined

            if (i < fogPoints.Count - 1) // We subtract 1 from fogpoints.count because the final triangle uses separate logic
            {
                meshTriangles[i * 3] = 0;
                meshTriangles[i * 3 + 1] = i + 1;
                meshTriangles[i * 3 + 2] = i + 2;
            }
        }

        // MANUALLY CREATE FINAL TRIANGLE
        meshTriangles[meshTriangles.Length - 3] = 0;
        meshTriangles[meshTriangles.Length - 2] = fogResolution;
        meshTriangles[meshTriangles.Length - 1] = 1;

        // REVERSE TRIANGLE INDICES SO NORMALS FACE THE CAMERA
        System.Array.Reverse(meshTriangles);

        // FEED ARRAYS INTO MESH
        mesh.vertices = meshVertices;
        mesh.triangles = meshTriangles;
    }
}