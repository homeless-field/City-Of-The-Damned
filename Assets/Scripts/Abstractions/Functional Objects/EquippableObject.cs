using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquippableObject : FunctionalObject
{
    // EQUIPPABLE OBJECTS CAN BE INSPECTED OR PICKED UP. WEAPONS, FOR EXAMPLE

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
