using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityObject : FunctionalObject
{
    // UTILITY OBJECTS CAN BE INSPECTED OR USED. GENERATORS, FOR EXAMPLE

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
