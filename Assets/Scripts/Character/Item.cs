using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public InventoryController.InventoryItems type;

    public void GetItem()
    {
        InventoryController.GetItem(type);
    }
}
