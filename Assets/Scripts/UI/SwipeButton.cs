using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SwipeButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Button button;

    Vector2 posStart;
    Vector2 posEnd;


    public void OnPointerDown(PointerEventData e)
    {
        posStart = e.position;
    }

    public void OnPointerUp(PointerEventData e)
    {
        posEnd = e.position;
        float dX = posEnd.x - posStart.x,
            dY = posEnd.y - posStart.y;

        if (Math.Abs(dX) > Math.Abs(dY))
        {
            if (dX > 0) MainController.ButtonPressed("Right");
            else MainController.ButtonPressed("Left");
        }
        else if (Math.Abs(dX) < Math.Abs(dY))
        {
            if (dY > 0) MainController.ButtonPressed("Up");
            else MainController.ButtonPressed("Down");
        }
    }
}
