using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    public static void ButtonPressed(string name)
    {
        if (GameController.isGameOn)
        {
            if (name.Contains("Inventory")) InventoryController.ButtonPressed(name);
            else
            {
                switch (name)
                {
                    case "Up":
                    case "Down":
                    case "Left":
                    case "Right":
                        MainCharacter.CharacterCommand(name);
                        break;
                }
            }
        }
        else
        {
            switch (name)
            {
                case "StartGame":
                    MainObjects.GameController.SetActive(true);
                    GameController.StartGame();
                    break;
            }
        }
    }
}
