using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    // PLACED ON MAIN CAMERA

    [SerializeField] Transform player;
    [SerializeField] float camMovementRange;

    private void LateUpdate()
    {
        // MOVE THE CAMERA RELATIVE TO THE PLAYER, TAKING THE CURSOR POSITION INTO ACCOUNT
        Vector2 mousePosPercent = new Vector2(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height);
        Vector2 cursorOffset = mousePosPercent - new Vector2(0.5f, 0.5f);
        Vector2 camOffset = (cursorOffset * camMovementRange) + new Vector2(player.position.x, player.position.y);

        transform.position = new Vector3(camOffset.x, camOffset.y, transform.position.z);
    }
}
