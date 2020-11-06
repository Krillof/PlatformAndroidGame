using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class PlatformPart : MonoBehaviour
{
    public GameObject platformPart = null;
    GameObject platform = null;
    GameObject content = null;

    float offset = 0;

    public Sprite[] sprites;
    SpriteRenderer sr = null;

    public Sprite[] spritesForAnimation;

    void Start()
    {
        sr = this.GetComponent<SpriteRenderer>();
    }

    public void GetOffsetToPlayer()
    {
        MainCharacter.PlatformOffset = offset;
    }

    public void GetPlatform(GameObject pl)
    {
        platform = pl;
    }

    public void SpawnMoney()
    {
        content = Transform.Instantiate(Prefabs.Coin, Prefabs.Coin.transform.position, Quaternion.identity);
        content.transform.parent = MainObjects.GameObjectsParent.transform;
    }

    public void SpawnBonus(object lineNumber)
    {
        if (!MainCharacter.startPlatformDetection) return;

        bool isBonusSpawned = true;

        int r = Random.Range(0, 15);

        if (r < Prefabs.BonusesExamples.Length)
        {
            r = Random.Range(0, 5);
            if (r <= 3)
            {
                r = Random.Range(0, 3);
            } else
            {
                r = Random.Range(0, Prefabs.BonusesExamples.Length);
            }


            if (r == Prefabs.LeftCannonBonusNumber)
            {
                if (((int)lineNumber) == 0)
                {
                    content = Transform.Instantiate(Prefabs.BonusesExamples[Prefabs.RightCannonBonusNumber],
                        Prefabs.BonusesExamples[Prefabs.RightCannonBonusNumber].transform.position, 
                        Quaternion.identity);
                } else
                {
                    content = Transform.Instantiate(
                        Prefabs.BonusesExamples[Prefabs.LeftCannonBonusNumber],
                        Prefabs.BonusesExamples[Prefabs.LeftCannonBonusNumber].transform.position, 
                        Quaternion.identity);
                }

            } else if (r == Prefabs.RightCannonBonusNumber)
            {
                if (((int)lineNumber) == 4)
                {
                    content = Transform.Instantiate(
                        Prefabs.BonusesExamples[Prefabs.LeftCannonBonusNumber],
                        Prefabs.BonusesExamples[Prefabs.LeftCannonBonusNumber].transform.position,
                        Quaternion.identity);
                }
                else
                {
                    content = Transform.Instantiate(Prefabs.BonusesExamples[Prefabs.RightCannonBonusNumber],
                        Prefabs.BonusesExamples[Prefabs.RightCannonBonusNumber].transform.position,
                        Quaternion.identity);
                }
            } else
                content = Transform.Instantiate(Prefabs.BonusesExamples[r], Prefabs.BonusesExamples[r].transform.position, Quaternion.identity);

            content.transform.parent = MainObjects.GameObjectsParent.transform;
        } else
        {
            isBonusSpawned = false;
        }

        if (isBonusSpawned) platform.SendMessage("BonusSpawned");

    }

    public void OnDestroy()
    {
        if (content != null)
        {
            Destroy(content);
        }
    }

    public void Move(Vector3 where)
    {
        platformPart.transform.position = where;

        if (content != null)
            content.transform.position = new Vector3(
                platformPart.transform.position.x,
                platformPart.transform.position.y + offset,
                platformPart.transform.position.z
                );
    }
}
