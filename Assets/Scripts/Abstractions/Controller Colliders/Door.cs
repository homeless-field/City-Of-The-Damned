using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : ControllerCollider
{
    // PLACED ON DOOR OBJECTS. ALLOWS THEM TO BE OPENED AND CLOSED BY CHARACTER CONTROLLERS

    [SerializeField] private Rigidbody rb;
    [SerializeField] private HingeJoint hinge;
    [SerializeField] private float timeToStayOpen, easeOfOpening;
    private bool opening = false;
    private float closeTimer;

    // OPEN THE DOOR A BIT AND RESTART A TIMER WHENEVER HIT BY A CHARACTER CONTROLLER
    public override void HitByController(ControllerColliderHit hit, int controllerLayer)
    {
        rb.velocity = new Vector3(hit.moveDirection.x, 0, 0) * easeOfOpening;
        closeTimer = timeToStayOpen;
        hinge.useSpring = false;
        opening = true;
    }

    private void Update()
    {
        // IF THE DOOR IS OPEN, CONSTANTLY COUNT DOWN A TIMER UNTIL WE CAN START TO CLOSE IT
        if (opening == false) return;

        closeTimer -= Time.deltaTime;
        if (closeTimer <= 0)
        {
            hinge.useSpring = true;
            opening = false;
        }
    }
}
