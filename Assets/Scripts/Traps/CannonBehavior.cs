using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CannonBehavior : MonoBehaviour
{
    public bool isLeft = false;
    public GameObject ThisCannon;

    public Sprite[] sprites;
    SpriteRenderer sr;

    int spriteNumber = 0;
    int interval = 0;
    int intervalMax = Config.maxIntervalBetweenCannonShots;

    void Start()
    {
        sr = this.GetComponent<SpriteRenderer>();
        sr.sprite = sprites[0];
    }

    void Update()
    {
        if (interval == intervalMax)
        {
            GameObject arrow = null;

            if (isLeft)
                arrow = Transform.Instantiate(Prefabs.LeftArrow, ThisCannon.transform.position, Quaternion.identity);
            else
                arrow = Transform.Instantiate(Prefabs.RightArrow, ThisCannon.transform.position, Quaternion.identity);

            arrow.transform.parent = MainObjects.GameObjectsParent.transform;

            interval = 0;
        }
        else
        {
            spriteNumber = Mathf.FloorToInt((((float)interval) / intervalMax) * sprites.Length);         
            sr.sprite = sprites[spriteNumber];
            if (spriteNumber == sprites.Length) spriteNumber = 0;
            

            interval++;
        }
    }

}
