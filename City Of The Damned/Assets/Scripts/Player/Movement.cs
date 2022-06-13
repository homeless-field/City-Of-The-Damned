using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // PLACED ON PLAYER. CONTROLS MOVEMENT OF THE CHARACTER CONTROLLER

    [SerializeField] private CharacterController charController;
    [SerializeField] private Transform groundChecker;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float movementSpeed, gravity, jumpForce, movementSpeedJumpMod, movementSpeedCrouchMult, jumpSpeedMultDuration, groundCheckerRadius;
    private Vector3 movementActual;
    private float movementSpeedCrouchMultActual = 1, movementSpeedJumpModActual, groundCheckerYPos, charControllerHeight;

    // "PUSHES" THE PLAYER ALONG THEIR PATH OF TRAVEL WHEN JUMPING
    private IEnumerator HorizontalJumpForce()
    {
        float lerpTime = 0;
        float originalVector = charController.velocity.x;

        while (lerpTime < 1.0f)
        {
            lerpTime += Time.deltaTime / jumpSpeedMultDuration;
            movementSpeedJumpModActual = Mathf.Lerp(originalVector * movementSpeedJumpMod, 0, lerpTime);

            yield return null;
        }
    }

    // SEND COLLISIONS TO CONTROLLERCOLLIDERS
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.TryGetComponent<ControllerCollider>(out ControllerCollider controllerCollider))
        {
            controllerCollider.HitByController(hit, gameObject.layer);
        }
    }

    private void Start()
    {
        groundCheckerYPos = groundChecker.localPosition.y;
        charControllerHeight = charController.height;
    }

    private void Update()
    {
        bool isGrounded = Physics.CheckSphere(groundChecker.position, groundCheckerRadius, groundMask);
        float movementInput = Input.GetAxis("Horizontal");
        movementActual = new Vector3((movementInput + movementSpeedJumpModActual) * movementSpeedCrouchMultActual, movementActual.y, 0);
        Debug.Log(movementSpeedCrouchMultActual);

        // JUMPING AND GRAVITY
        if (isGrounded)
        {
            movementActual.y = 0;
            if (Input.GetButtonDown("Jump"))
            {
                movementActual.y = jumpForce;
                StartCoroutine("HorizontalJumpForce");
            }
        }
        else
        {
            movementActual.y += gravity * Time.deltaTime;
        }

        // MOVE THE CHARACTER CONTROLLER
        charController.Move(new Vector3(movementActual.x, movementActual.y, 0) * movementSpeed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);

        // CROUCHING
        if (Input.GetButtonDown("Crouch"))
        {
            movementSpeedCrouchMultActual = movementSpeedCrouchMult;
            groundChecker.localPosition = new Vector3(groundChecker.localPosition.x, 0, groundChecker.localPosition.z);
            charController.height = charControllerHeight / 2;
            transform.position = new Vector3(transform.position.x, transform.position.y - (charControllerHeight / 4), transform.position.z);
            Physics.SyncTransforms();
        }

        if (Input.GetButtonUp("Crouch"))
        {
            movementSpeedCrouchMultActual = 1;
            groundChecker.localPosition = new Vector3(groundChecker.localPosition.x, groundCheckerYPos, groundChecker.localPosition.z);
            charController.height = charControllerHeight;
            transform.position = new Vector3(transform.position.x, transform.position.y + (charControllerHeight / 4), transform.position.z);
            Physics.SyncTransforms();
        }

    }
}