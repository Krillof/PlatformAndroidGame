using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ArrowBehavior : MonoBehaviour
{
    public Sprite[] ArrowSprites;

    public bool isLeft = false;
    public GameObject ThisArrow;

    int interval = 0;
    int intervalMax = 5;

    int spriteInterval = 0;
    int spriteIntervalMax = 7;

    int spriteIndex = 0;

    // Update is called once per frame
    void Update()
    {
        if (!GameController.isGameOn) Destroy(ThisArrow);

        ThisArrow.transform.position = new Vector3(
                ThisArrow.transform.position.x + ((isLeft) ? (-1) : (1)) * Config.arrowSpeed,
                ThisArrow.transform.position.y,
                ThisArrow.transform.position.z
                );

        if (interval == 0)
        {

            if (math.abs(ThisArrow.transform.position.x) > Config.destroyArrowOnDistanceX)
            {
                Destroy(ThisArrow);
            }

            interval = intervalMax;
        }
        else interval--;


        //Animation
        if (spriteInterval == 0)
        {
            SpriteRenderer sr = this.GetComponent<SpriteRenderer>();

            sr.sprite = ArrowSprites[spriteIndex];

            spriteIndex++;
            if (spriteIndex == ArrowSprites.Length)
            {
                spriteIndex = 0;
            }

            spriteInterval = spriteIntervalMax;
        }
        else spriteInterval--;
    }
}
