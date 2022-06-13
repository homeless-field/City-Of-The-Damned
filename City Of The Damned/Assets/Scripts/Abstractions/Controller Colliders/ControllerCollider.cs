using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ControllerCollider : MonoBehaviour
{
    // REPRESENTS OBJECTS THAT HAVE COLLISION LOGIC WITH CHARACTER CONTROLLERS

    // CALLED FROM A CHARACTER CONTROLLER WHEN IT COLLIDES WITH A CONTROLLERCOLLIDER
    public abstract void HitByController(ControllerColliderHit hit, int controllerLayer);
}
