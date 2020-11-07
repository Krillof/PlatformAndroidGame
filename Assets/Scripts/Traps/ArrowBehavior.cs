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
    int intervalMax = 100;

    bool isCoroutineStarted = false;

    float acceleration = (Config.arrowSpeedX /150);
    float speed = Config.arrowSpeedX;

    int spriteInterval = 0;
    int spriteIntervalMax = 7;

    int spriteIndex = 0;

    IEnumerator SpeedDown()
    {
        while (interval <= intervalMax)
        {
            if (interval == intervalMax)
            {
                this.GetComponent<SpriteRenderer>().sortingOrder = -5;
            }
            else
            {
                speed = speed - acceleration;
                interval++;
            }
            yield return new WaitForSeconds(0.01f);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (!isCoroutineStarted) StartCoroutine(SpeedDown());
        if (GameController.isGameStop) return;
        if (!GameController.isGameOn) Destroy(ThisArrow);

        ThisArrow.transform.position = new Vector3(
                ThisArrow.transform.position.x + ((isLeft) ? (-1) : (1)) * speed,
                ThisArrow.transform.position.y,
                ThisArrow.transform.position.z
        );

        if (math.abs(ThisArrow.transform.position.x) > Config.destroyArrowOnDistanceX)
        {
            Destroy(ThisArrow);
        }

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
