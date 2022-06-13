using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // PLACED ON THE GAME MANAGER

    [SerializeField] private Texture2D defaultCursor;
    private Vector2 cursorHotspot = new Vector2(17, 5);

    private void Start()
    {
        // SET THE CUSTOM CURSOR
        Cursor.SetCursor(defaultCursor, cursorHotspot, CursorMode.Auto);
    }
}
