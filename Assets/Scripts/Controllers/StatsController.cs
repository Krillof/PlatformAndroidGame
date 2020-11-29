using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsController : MonoBehaviour
{
    public Text _MoneyText;
    public static Text MoneyText;

    public Text _CurrentShieldStat;
    public Text _CurrentDisableVoltageStat;
    public Text _CurrentFreezeStat;

    public static Text CurrentShieldStat;
    public static Text CurrentDisableVoltageStat;
    public static Text CurrentFreezeStat;

    void Start()
    {
        MoneyText = _MoneyText;
        CurrentShieldStat = _CurrentShieldStat;
        CurrentFreezeStat = _CurrentFreezeStat;
        CurrentDisableVoltageStat = _CurrentDisableVoltageStat;
    }

    private static void UpdateNumbers()
    {
        MoneyText.text = "Your money: " + Convert.ToString(InfoSaver.SavedData.Money);

        CurrentShieldStat.text = "Current: " + Convert.ToString(InfoSaver.SavedData.ShieldStat);
        CurrentFreezeStat.text = "Current: " + Convert.ToString(InfoSaver.SavedData.FreezeStat);
        CurrentDisableVoltageStat.text = "Current: " + Convert.ToString(InfoSaver.SavedData.DisableVoltageStat);
    }

    public static void GoToStats()
    {
        MainObjects.Camera.transform.position = MainObjects.StatsController.transform.position;

        UpdateNumbers();
    }

    public static void UpFreeze()
    {
        InfoSaver.SavedData.FreezeStat++;
        InfoSaver.Save();
        UpdateNumbers();
    }

    public static void UpDisableVoltage()
    {
        InfoSaver.SavedData.DisableVoltageStat++;
        InfoSaver.Save();
        UpdateNumbers();
    }

    public static void UpShield()
    {
        InfoSaver.SavedData.ShieldStat++;
        InfoSaver.Save();
        UpdateNumbers();
    }

    public static void BackToMenu()
    {
        MainObjects.Camera.transform.position = new Vector3(0,1, -10);
    }
}
