using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreTeleportCheckerObject : MonoBehaviour
{
    void Start()
    {
        Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), MainObjects.Player.GetComponent<Collider2D>());
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "PlatformPart")
        {
            PreTeleportCheck.isGoodPlace = true;
        }
    }
}
