using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryObject : FunctionalObject
{
    // INVENTORY OBJECTS CAN BE INSPECTED OR ADDED TO YOUR INVENTORY. AMMO, FOR EXAMPLE

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
