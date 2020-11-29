using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    public static void ButtonPressed(string name)
    {
        if (GameController.isGameStop)
        {
            switch (name)
            {
                case "Unstop":
                    MainObjects.TutorialDialogs.SendMessage("Unstop");
                    break;
            }
        } else if (GameController.isGameOn)
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
        } else
        {
            switch (name)
            {
                case "StartGame":
                    MainObjects.GameController.SetActive(true);
                    GameController.StartGame();
                    break;
                case "Stats":
                    StatsController.GoToStats();
                    break;
                case "StatsBack":
                    StatsController.BackToMenu();
                    break;
                case "UpFreeze":
                    StatsController.UpFreeze();
                    break;
                case "UpDisableVoltage":
                    StatsController.UpDisableVoltage();
                    break;
                case "UpShield":
                    StatsController.UpShield();
                    break;
            }
        }
    }
}
