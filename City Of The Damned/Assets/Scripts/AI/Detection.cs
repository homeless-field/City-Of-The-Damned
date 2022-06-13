using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{
    // PLACED ON AI CHARACTERS. SIMULATES THE "SENSES", SUCH AS EYESIGHT OR HEARING

    [SerializeField] private Eyesight eyesight;
    [SerializeField] private Transform player;
    [SerializeField] private CharacterController playerCharController;
    private Vector3[] playerPointsLocal = new Vector3[6];
    private int playerLayer;

    // CLASSES REPRESENTING THE VARIOUS SENSES
    private class Sense
    {
        public float range;
        public Transform transform;
    }

    [System.Serializable]
    private class Eyesight : Sense
    {
        public Vector2 fieldOfView;
        public LayerMask layerMask;
    }

    private void Start()
    {
        playerLayer = LayerMask.NameToLayer("Player");
        eyesight.fieldOfView /= 2; // We divide eyesight.fieldOfView by 2 because a 90 degree FOV means 2 45 degree angles

        // GET THE LOCAL POSITION OF THE PLAYER BOUNDS. IT'S ASSUMED THESE ARE CONSISTENT. THEY'RE INSET A BIT TO PREVENT EDGECASES
        Vector3 playerBounds = playerCharController.bounds.extents;
        playerPointsLocal[0] = (playerBounds.x -  0.1f) * Vector3.left;
        playerPointsLocal[1] = (playerBounds.x - 0.1f) * Vector3.right;
        playerPointsLocal[2] = (playerBounds.y - 0.3f) * Vector3.down;
        playerPointsLocal[3] = (playerBounds.y - 0.3f) * Vector3.up;
        playerPointsLocal[4] = (playerBounds.z - 0.1f) * Vector3.back;
        playerPointsLocal[5] = (playerBounds.z - 0.1f) * Vector3.forward;
    }

    private void Update()
    {
        List<float> playerAngles = new List<float>();

        // SEND A RAYCAST TO EACH "LIMIT" OF PLAYER BOUNDS
        float nearestPlayerAngle = Mathf.Infinity;
        for (int i = 0; i < 6; i++)
        {
            // FIND THE CENTER-MOST RAYCAST THAT HITS THE PLAYER
            Ray eyeRay = new Ray(eyesight.transform.position, ((playerPointsLocal[i] + player.position) - eyesight.transform.position));
            if (Physics.Raycast(eyeRay, out RaycastHit hit, eyesight.range, eyesight.layerMask) && hit.collider.gameObject.layer == playerLayer)
                {
                    float angle = Vector3.Angle(eyesight.transform.forward, eyeRay.direction);
                    if (angle < nearestPlayerAngle)
                        nearestPlayerAngle = angle;
                }
        }


        // DETECT THE PLAYER
        if (nearestPlayerAngle < eyesight.fieldOfView[0])
        {
            Debug.Log("Spotted");
        }
        else if (nearestPlayerAngle < eyesight.fieldOfView[1])
        {
            Debug.Log("Hunted");
        }
    }
}
