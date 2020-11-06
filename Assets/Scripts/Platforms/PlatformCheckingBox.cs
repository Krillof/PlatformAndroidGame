using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCheckingBox : MonoBehaviour
{
    public GameObject platformCheckingBox;

    public void StartChecking()
    {
        if (platformCheckingBox.tag != "+")
            platformCheckingBox.tag = "-";
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        switch (collider.gameObject.tag)
        {
            case "PlatformPart":
                platformCheckingBox.tag = "+";
                break;
        }
    }
}
