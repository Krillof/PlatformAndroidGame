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
    int intervalMax = 150;

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
            int k = ((int)((( (float) interval) / intervalMax) * 100));
            int j = ((int)((((float) spriteNumber) / sprites.Length) * 100));
            if (k >= j)
            {
                sr.sprite = sprites[spriteNumber++];
                if (spriteNumber == sprites.Length) spriteNumber = 0;
            }

            interval++;
        }
    }

}
