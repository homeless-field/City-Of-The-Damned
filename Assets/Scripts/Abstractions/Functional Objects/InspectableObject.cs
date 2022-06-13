using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectableObject : FunctionalObject
{
    // INSPECTABLE OBJECTS CAN ONLY BE INSPECTED. POSTERS, FOR EXAMPLE

    [SerializeField] private Texture2D hoverCursor;

    private void OnMouseEnter()
    {
        OnMouseHover(hoverCursor);
    }

    private void OnMouseExit()
    {
        OnMouseUnhover();
    }
}
