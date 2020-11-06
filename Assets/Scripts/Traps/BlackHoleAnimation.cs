using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleAnimation : MonoBehaviour
{
    public Sprite[] sprites;

    int spriteNumber = 0;
    int spriteInterval = 0;
    int spriteIntervalMax = 10;

    SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (spriteInterval == spriteIntervalMax)
        {
            sr.sprite = sprites[spriteNumber++];
            if (spriteNumber >= sprites.Length) spriteNumber = 0;

            spriteInterval = 0;
        }
        else spriteInterval++;
    }
}
