using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ReturningDraggableObject : MonoBehaviour, IPointerDownHandler
{

    float StartX;
    float StartY;
    float MovingOffsetX;
    float MovingOffsetY;
    public int ButtonNumber = -1;
    bool isNotDoDrag = false;

    public static int LastButtonNumber = -1;
    public static float LastEndX;
    public static float LastEndY;

    public void OnPointerDown(PointerEventData e)
    {

        int itemNumber = ButtonNumber - 1 + InventoryController.Offset;

        if (itemNumber < InventoryController.Items.Count)
        {
            InventoryController.CellItem item = InventoryController.Items[itemNumber];

            if (item.type == InventoryController.InventoryItems.Freeze ||
                item.type == InventoryController.InventoryItems.IgnoreTraps ||
                item.type == InventoryController.InventoryItems.DisableVoltage)
            {
                InventoryController.ItemUsed(ButtonNumber);
                return;
            }
        }
    }

    public void BeginDrag()
    {
        int itemNumber = ButtonNumber - 1 + InventoryController.Offset;
        if ( itemNumber < InventoryController.Items.Count)
        {
            InventoryController.CellItem item = InventoryController.Items[itemNumber];

            if (item.type == InventoryController.InventoryItems.Freeze ||
                item.type == InventoryController.InventoryItems.IgnoreTraps)
            {
                isNotDoDrag = true;
                return;
            }
        }

        StartX = transform.position.x;
        StartY = transform.position.y;

        MovingOffsetX = MainObjects.GameCanvas.transform.position.x;
        MovingOffsetY = MainObjects.GameCanvas.transform.position.y;
    }

    public void EndDrag()
    {
        if (isNotDoDrag)
        {
            isNotDoDrag = false;
            return;
        }

        LastEndX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        LastEndY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;

        InventoryController.ItemUsed(ButtonNumber);

        MovingOffsetX = MainObjects.GameCanvas.transform.position.x - MovingOffsetX;
        MovingOffsetY = MainObjects.GameCanvas.transform.position.y - MovingOffsetY;

        transform.position = new Vector3(StartX + MovingOffsetX, StartY + MovingOffsetY);
    }

    public void OnMouseDrag()
    {
        if (isNotDoDrag)
        {
            return;
        }

        transform.position = new Vector3(
            Camera.main.ScreenToWorldPoint(Input.mousePosition).x, 
            Camera.main.ScreenToWorldPoint(Input.mousePosition).y
            );
    }
}
