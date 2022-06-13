using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelManager : MonoBehaviour
{
    // PLACED ON PLAYER MODEL. ALLOWS THE MODEL TO HAVE LOGIC SEPARATE FROM THE COLLIDER

    private void LateUpdate()
    {
        float movementVector = Input.GetAxis("Horizontal");

        // ROTATE TO FACE THE MOVEMENT DIRECTION
        if (movementVector < 0)
            transform.SetPositionAndRotation(transform.position, Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z));
        else if (movementVector > 0)
            transform.SetPositionAndRotation(transform.position, Quaternion.Euler(transform.rotation.x, 180, transform.rotation.z));
    }
}
