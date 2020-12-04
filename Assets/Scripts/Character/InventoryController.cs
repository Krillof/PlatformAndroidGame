using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    public Image[] InventoryCells;
    public Sprite[] ItemsSprites;
    public Text[] ItemsAmounts;

    public static Image[] InventoryCellsPublic;
    public static Sprite[] ItemsSpritesPublic;
    public static Text[] ItemsAmountsPublic;
    public static int Offset = 0;
    public static List<CellItem> Items;
    public static CellItem[] ShowingItems;

    public static InventoryController InventoryControllerObject;

    static bool isNotRemoveItem = false;

    public enum InventoryItems
    {
        None = 0,
        IgnoreTraps = 1, 
        Teleport = 2,
        Freeze = 3,
        DisableVoltage = 4,
        PlatformSpawner = 5
    }

    public class CellItem
    {
        public InventoryItems type = InventoryItems.None;
        public int amount = 1;

        public CellItem(InventoryItems Type)
        {
            type = Type;
        }
    }

    public static void ItemAction(InventoryItems item)
    {
        switch (item)
        {
            case InventoryItems.IgnoreTraps:
                MainCharacter.ignoreTrapsStartTime = GameController.gameTime;
                MainCharacter.ignoreTraps = true;
                break;
            case InventoryItems.Teleport:
                PreTeleportCheck.CheckPlace(ReturningDraggableObject.LastEndX, ReturningDraggableObject.LastEndY);
                InventoryControllerObject.SendMessage("TeleportItem_Action");
                break;
            case InventoryItems.Freeze:
                MainCharacter.freezeStartTime = GameController.gameTime;
                MainCharacter.freeze = true;
                break;
            case InventoryItems.DisableVoltage:
                MainObjects.TopPad.SendMessage("ResetAccumulatorsValues");
                MainCharacter.ignoreAccumulationStartTime = GameController.gameTime;
                MainCharacter.ignoreAccumulation = true;
                break;
            case InventoryItems.PlatformSpawner:
                int line = GameController.FloatToLine(ReturningDraggableObject.LastEndX);
                if (line != -1)
                {
                    GameController.SpawnIndividualPlatform(ReturningDraggableObject.LastEndY, line, () =>
                    {
                        InventoryController.GetItem(InventoryItems.PlatformSpawner);
                    });
                } else InventoryController.isNotRemoveItem = true;

                break;
        }
    }

    public void TeleportItem_Action()
    {
        StartCoroutine(TeleportItem__Action());
    }

    IEnumerator TeleportItem__Action()
    {
        yield return new WaitForSeconds(0.1f);

        if (PreTeleportCheck.isGoodPlace)
            MainCharacter.Teleport(ReturningDraggableObject.LastEndX, ReturningDraggableObject.LastEndY);
        else
            isNotRemoveItem = true;

        PreTeleportCheck.isGoodPlace = false;
    }

    void Start()
    {
        InventoryControllerObject = this;

        Items = new List<CellItem>();
        ShowingItems = new CellItem[InventoryCells.Length];
        for (int f = 0; f < ShowingItems.Length; f++)
        {
            ShowingItems[f] = null;
        }
        InventoryCellsPublic = InventoryCells;
        ItemsSpritesPublic = ItemsSprites;
        ItemsAmountsPublic = ItemsAmounts;

        GetItem(InventoryItems.IgnoreTraps);
        GetItem(InventoryItems.Teleport);
        GetItem(InventoryItems.Freeze);

        ShowItems();
    }
    

    static public void ButtonPressed(string name)
    {
        switch (name)
        {
            case "InventoryRightButton":
                //Offset++ with conditions
                if (Items.Count != Offset + 1)
                    Offset++;
                ShowItems();
                break;
            case "InventoryLeftButton":
                //Offset-- with conditions
                if (Offset > 0)
                    Offset--;
                ShowItems();
                break;
        }
    }

    static public void ShowItems()
    {
        for (int f = 0; f < InventoryCellsPublic.Length; f++)
        {
            ShowingItems[f] = (f + Offset < Items.Count) ? (Items[f + Offset]) : (null);
            InventoryCellsPublic[f].sprite = ItemsSpritesPublic[(ShowingItems[f] != null) ? ((int)ShowingItems[f].type) : (0)];
            ItemsAmountsPublic[f].text = (ShowingItems[f] != null) ? (Convert.ToString(ShowingItems[f].amount)) : ("");
        }
    }

    static public void GetItem(InventoryItems itemType)
    {
        bool ItemExists = false;

        for (int f = 0; f < Items.Count; f++)
        {
            if (Items[f].type == itemType)
            {
                Items[f].amount++;
                ItemExists = true;
            }
        }

        if (!ItemExists)
        {
            Items.Add(new CellItem(itemType));
        }
        
        ShowItems();
    }

    static public void ItemUsed(int buttonNumber)
    {
        int ItemIndex = buttonNumber + Offset - 1;
        bool isLastItem = false;

        if (ItemIndex < Items.Count)
        {
            ItemAction(Items[ItemIndex].type);

            if (!isNotRemoveItem)
            {
                Items[ItemIndex].amount--;

                if (Items[ItemIndex].amount == 0)
                {
                    Items.RemoveAt(ItemIndex);

                    isLastItem = true;
                }
            } else isNotRemoveItem = false;

            if (isLastItem)
            {
                ShowingItems[ItemIndex-Offset] = null;
            }

            ShowItems();
        }
    }
}
