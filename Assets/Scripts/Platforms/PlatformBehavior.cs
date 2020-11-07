using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlatformBehavior : MonoBehaviour
{
    public GameObject Platform;
    public GameObject[] PlatformParts;

    GameObject[] PlatformConnectors;
    GameObject Fires;
    SpriteRenderer FiresSR;

    int Line = 0;
    float speed = Config.platformsSpeed;
    bool isMoving = false;
    bool isNoContent = false;
    float x;
    float endY;
    bool isBonusSpawned = false;

    int fireIterations = 0;
    int fireIterationsMax = 10;
    int currentSpriteNumber = 0;

    public void BonusSpawned()
    {
        isBonusSpawned = true;
    }

    public void AddLineSpeed(int line)
    {
        Line = line;
        speed += Config.lineSpeedDiff * line;
    }

    public void SetYDirection(bool isDown)
    {
        if (!isDown) speed = -speed;
    }

    public void SetEndY(float EndY)
    {
        endY = EndY;
    }

    public void SetNoContent(bool isYes)
    {
        isNoContent = isYes;
    }

    public void StartMoving(float X)
    {
        x = X;

        int moneyIndex = Random.Range(0, PlatformParts.Count());

        for (int f = 0; f < PlatformParts.Count(); f++)
        {
            PlatformParts[f] = Transform.Instantiate(PlatformParts[f], PlatformParts[f].transform.position, Quaternion.identity);
            PlatformParts[f].transform.parent = Platform.transform;
            PlatformParts[f].SendMessage("GetPlatform", Platform);
            
            if (!isNoContent)
                if (moneyIndex == f) 
                    PlatformParts[f].SendMessage("SpawnMoney");
                else
                    if (!isBonusSpawned) PlatformParts[f].SendMessage("SpawnBonus", Line);
        }

        PlatformConnectors = new GameObject[PlatformParts.Count() - 1];

        for (int f = 0; f < PlatformParts.Count()-1; f++)
        {
            PlatformConnectors[f] = Transform.Instantiate(Prefabs.PlatformConnectors, Prefabs.PlatformConnectors.transform.position, Quaternion.identity);
            PlatformConnectors[f].transform.parent = Platform.transform;
        }

        Fires = new GameObject();
        Fires.transform.parent = Platform.transform;
        FiresSR = Fires.AddComponent<SpriteRenderer>();
        FiresSR.sortingOrder = -7;

        isMoving = true;
    }

    public void DestroyPlatform()
    {
        foreach (var el in PlatformParts)
        {
            el.SendMessage("OnDestroy");
            Destroy(el);
        }
        foreach (var el in PlatformConnectors)
        {
            Destroy(el);
        }
        Destroy(Fires);
        isMoving = false;
        GameController.ExcludePlatform(Platform);
        Destroy(Platform);
        Platform.SetActive(false);
    }

    public void Move()
    {
        if (isMoving && GameController.isGameOn)
        {

            float k = speed + ((speed > 0) ? (1) : (-1)) 
                * MainObjects.Player.transform.position.y 
                * Config.accelerationCoefficent;

            if (k > Config.speedMax) k = Config.speedMax;

            if (MainCharacter.freezeIterations > 0) k -= speed / 2;

            Platform.transform.position = new Vector3(
                x,
                Platform.transform.position.y
                    + k,
                Platform.transform.position.z
                );



            if (speed > 0)
            {
                if (Platform.transform.position.y 
                    >=
                    MainObjects.Player.transform.position.y + endY)
                {
                    DestroyPlatform();
                }
            }
            else
            {
                if (Platform.transform.position.y 
                    <=
                     MainObjects.Player.transform.position.y - endY)
                {
                    DestroyPlatform();
                }
            }

            float m = 1.05f;
            float firesHeight = 0.7f;

            for (int f = 0; f < PlatformParts.Count(); f++)
            {
                PlatformParts[f].SendMessage("Move", 
                    new Vector3(
                        Platform.transform.position.x,
                         Platform.transform.position.y + f * m,
                         Platform.transform.position.z
                    ),
                    SendMessageOptions.DontRequireReceiver
                );
            }

            for (int f = 0; f < PlatformConnectors.Count(); f++)
            {
                PlatformConnectors[f].transform.position = new Vector3(
                    Platform.transform.position.x,
                     Platform.transform.position.y + (f+1) * m - (m/2),
                     Platform.transform.position.z
                );
            }

            if (speed > 0)
            {
                Fires.transform.position = new Vector3(
                    Platform.transform.position.x,
                     Platform.transform.position.y - m/2 - firesHeight/2,
                     Platform.transform.position.z);
            } else
            {
                Fires.transform.position = new Vector3(
                    Platform.transform.position.x,
                     Platform.transform.position.y + (PlatformParts.Count()-1) * m + (m / 2) + firesHeight / 2,
                     Platform.transform.position.z);
            }

            
        }
    }

    void Update()
    {
        if (fireIterations == fireIterationsMax)
        {
            
            if (speed > 0)
            {
                currentSpriteNumber = (currentSpriteNumber+1) % Prefabs.FiresUp.Length;
                FiresSR.sprite = Prefabs.FiresUp[currentSpriteNumber];
            }
            else
            {
                currentSpriteNumber = (currentSpriteNumber + 1) % Prefabs.FiresDown.Length;
                FiresSR.sprite = Prefabs.FiresDown[currentSpriteNumber];
            }

            fireIterations = 0;
        }
        else fireIterations++;


        if (GameController.destroyAllGameObjects)
        {
            DestroyPlatform();
        }
    }
}
