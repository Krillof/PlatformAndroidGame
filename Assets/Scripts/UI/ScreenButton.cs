using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScreenButton : MonoBehaviour, IPointerDownHandler
{
    public string Name;


    public void OnPointerDown(PointerEventData e)
    {
        MainController.ButtonPressed(Name);
    }
}
