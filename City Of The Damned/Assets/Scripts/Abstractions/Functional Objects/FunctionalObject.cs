using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FunctionalObject : MonoBehaviour
{
    // REPRESENTS OBJECTS THAT HAVE HAVE SOME KIND OF INTERACTABILITY WITHIN THE WORLD

    private Texture2D currentCursor;
    [SerializeField] private Texture2D defaultCursor;
    private Vector2 cursorHotspot = new Vector2(17, 5);

    // CALLED BY A FUNCTIONALOBJECT WHEN THE USER HOVERS OVER IT
    public virtual void OnMouseHover(Texture2D newCursor)
    {
        // SET THE CURSOR TO THE CORRECT ONE FOR THIS OBJECT
        Cursor.SetCursor(newCursor, cursorHotspot, CursorMode.Auto);
    }

    // CALLED BY A FUNCTIONALOBJECT WHEN THE USER STOPS HOVERING OVER IT
    public virtual void OnMouseUnhover()
    {
        // SET THE CURSOR TO DEFAULT
        Cursor.SetCursor(defaultCursor, cursorHotspot, CursorMode.Auto);
    }
}
