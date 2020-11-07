using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Linq;
using UnityEditor;

using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameController : MonoBehaviour
{
    static List<GameObject> platforms = new List<GameObject>();
    static public bool isGameOn = false;
    static public bool isGameStop = false;
    static public bool destroyAllGameObjects = true;
    bool isFreezeImageBlue = false;

    public static int IndividualPlatformLine = 0;
    public static float IndividualPlatformY = 0f;
    public delegate void IndividualPlatformSpawnFailureDelegate();
    public static IndividualPlatformSpawnFailureDelegate IndividualPlatformSpawnFailure;

    void Start()
    {
        StartCoroutine(SpawnGameCycle());
        StartCoroutine(MovingGameCycle());
    }

    public static int FloatToLine(float x)
    {
        for (int f = 0; f < Config.linesAmount; f++)
        {
            float lineStart = CountXPlace(f) - Config.linesXSize / 2;
            float lineEnd = CountXPlace(f+1) - Config.linesXSize / 2;
            if ((lineStart < x)&&(x < lineEnd))
            {
                return f;
            }
        }
        return -1;
    }

    public static float CountXPlace(int lineIndex)
    {
        return Config.linesXStart + (1 + Config.linesSpaceBetween) * lineIndex;
    }

    public static float CountYStartPlace(int lineIndex, bool isInverted = false)
    {
        bool b = false;
        b = (lineIndex % 2 == 0);
        if (isInverted) b = !b;
        
        return ( (b) ? (-Config.platformStartSpawnRelativeY) : 
            (Config.platformStartSpawnRelativeY)) + 
            MainObjects.Player.transform.position.y;
    }

    public static void StartGame()
    {
        print("start game");
        destroyAllGameObjects = false;
        MainObjects.GameCanvas.SetActive(true);
        MainObjects.MenuCanvas.SendMessage("StartGame");

        MainCharacter.GameStarted();

        MainCharacter.MoneyCount = InfoSaver.SavedData.Money;

        BackgroundMusic.InGame();
        isGameOn = true;
        MainObjects.GameController.SendMessage("SpawnStartPlatform");

        if (!InfoSaver.SavedData.isSeenPlayer)
        {
            MainObjects.TutorialDialogs.SendMessage("ShowPlayerDialog");
        }
    }

    public static void EndGame()
    {
        print("end game");

        MainObjects.GameController.SendMessage("StartMoveToStart");
    }

    void StartMoveToStart()
    {
        StartCoroutine(MoveToStart());
    }

    IEnumerator MoveToStart()
    {

        yield return new WaitForSeconds(2f);

        MainObjects.MenuCanvas.SendMessage("EndGame");
        MainObjects.TopPad.SendMessage("SetText", $"Best: {InfoSaver.SavedData.MaxDistance}");
        isGameOn = false;
        MainObjects.GameCanvas.SetActive(false);
        MainCharacter.freezeIterations = 0;
        MainCharacter.ignoreTrapsIterations = 0;
        destroyAllGameObjects = true;
        MainObjects.TopPad.SendMessage("ResetAccumulatorsValues");
        BackgroundMusic.InMenu();

        InfoSaver.SavedData.Money = MainCharacter.MoneyCount;
        InfoSaver.Save();

        MainObjects.Player.transform.position = new Vector3(
            0,
            1,
            MainObjects.Player.transform.position.z
            );

        CameraBehavior.CameraObject.transform.position = new Vector3(
            0,
            1,
            CameraBehavior.CameraObject.transform.position.z
            );

        MainObjects.GameCanvas.transform.position = new Vector3(
            0,
            1,
             MainObjects.GameCanvas.transform.position.z
            );

        MainObjects.StaticCanvas.transform.position = new Vector3(
            0,
            1,
             MainObjects.GameCanvas.transform.position.z
            );
    }

    public static void ExcludePlatform(GameObject pl)
    {
        platforms.Remove(pl);
    }

    public void SpawnStartPlatform()
    {
        int lineIndex = 2;
        int r;
        GameObject platform;

        r = UnityEngine.Random.Range(0, Prefabs.PlatformExamples.Count());
        platform = Transform.Instantiate(Prefabs.PlatformExamples[r], Prefabs.PlatformExamples[r].transform.position, Quaternion.identity);
        platform.transform.parent = MainObjects.GameObjectsParent.transform;

        platform.SendMessage("AddLineSpeed", lineIndex);
        platform.SendMessage("SetEndY", Config.platformDestroyRelativeY);
        platform.SendMessage("SetYDirection", lineIndex % 2 == 0);
        platform.transform.position = new Vector3(platform.transform.position.x,
                1,
                platform.transform.position.z);



        platform.SendMessage("StartMoving", CountXPlace(lineIndex));

        platforms.Add(platform);
    }

    void SpawnPlatform(int lineIndex, float y, bool isNoContent = false, int platformExample = -1)
    {
        int r;
        GameObject platform;

        if (platformExample < 0)
            r = UnityEngine.Random.Range(0, Prefabs.PlatformExamples.Count());
        else
            r = platformExample;

        platform = Transform.Instantiate(Prefabs.PlatformExamples[r], Prefabs.PlatformExamples[r].transform.position, Quaternion.identity);
        platform.transform.parent = MainObjects.GameObjectsParent.transform;

        platform.SendMessage("AddLineSpeed", lineIndex);
        platform.SendMessage("SetEndY", Config.platformDestroyRelativeY);
        platform.SendMessage("SetYDirection", lineIndex%2==0);
        platform.transform.position = new Vector3(platform.transform.position.x,
                y,
                platform.transform.position.z);


        platform.SendMessage("SetNoContent", isNoContent);
        platform.SendMessage("StartMoving", CountXPlace(lineIndex));

        platforms.Add(platform);
    }

    public static void SpawnIndividualPlatform(float y, int line, IndividualPlatformSpawnFailureDelegate failure)
    {
        IndividualPlatformLine = line;
        IndividualPlatformY = y;
        IndividualPlatformSpawnFailure = failure;
        MainObjects.GameController.SendMessage("StartSpawningIndividualPlatform");
    }

    public void StartSpawningIndividualPlatform()
    {
        StartCoroutine(SpawnIndividualPlatform());
    }

    IEnumerator SpawnIndividualPlatform()
    {
        GameObject checkBox =
                       Transform.Instantiate(Prefabs.PlatformCheckingBox,
                       new Vector3(CountXPlace(IndividualPlatformLine),
                       IndividualPlatformY,
                       Prefabs.PlatformCheckingBox.transform.position.z),
                       Quaternion.identity);

        checkBox.transform.parent = MainObjects.GameObjectsParent.transform;

        checkBox.SendMessage("StartChecking");

        for (int f = 0; f < 10; f++)
        {
            checkBox.transform.position = new Vector3(
                checkBox.transform.position.x + 0.04f,
                checkBox.transform.position.y,
                checkBox.transform.position.z
                );
            yield return new WaitForSeconds(0.01f);
        }

        if (checkBox.tag == "-")
            SpawnPlatform(IndividualPlatformLine, IndividualPlatformY, true, 0);
        else 
            IndividualPlatformSpawnFailure();

        Destroy(checkBox);
    }

    IEnumerator MovingGameCycle()
    {
        int k = 0;
        while (true)
        {
            if (isGameOn && (!isGameStop))
            {
                k = platforms.Count;
                for (int f = 0; f < k; f++)
                {
                    if (f < platforms.Count)
                        platforms[f].SendMessage("Move");
                }

                if (MainObjects.Player.activeSelf)
                    MainObjects.Player.SendMessage("Move");

                if (MainObjects.TopPad.activeSelf && MainObjects.GameCanvas.activeSelf)
                    MainObjects.TopPad.SendMessage("Moving");


                if (MainCharacter.freezeIterations > 0)
                {
                    MainCharacter.freezeIterations -= 1;
                    if (!isFreezeImageBlue) {
                        Prefabs.FreezeImage.color = new Color(0, 0, 255, 0.1f);
                        isFreezeImageBlue = true;
                    }
                }
                else if (isFreezeImageBlue)
                {
                    Prefabs.FreezeImage.color = new Color(0, 0, 0, 0);
                    isFreezeImageBlue = false;
                }

                if (MainCharacter.ignoreTrapsIterations > 0)
                {
                    MainCharacter.ignoreTrapsIterations--;
                }

                //StoppingTrapSpawningObject.SendMessage("ControlMarks");

                MainObjects.TopPad.SendMessage("Accumulate");
            }

            yield return new WaitForSeconds(Config.movingGameCycleTime);
        }
    }

    IEnumerator SpawnGameCycle()
    {
        while (true){
            if (isGameOn && (!isGameStop))
            {
                for (int lineIndex = 0; lineIndex < Config.linesAmount; lineIndex++)
                {

                    GameObject checkBox =
                        Transform.Instantiate(Prefabs.PlatformCheckingBox,
                        new Vector3(CountXPlace(lineIndex),
                            ((lineIndex % 2 == 0) ? 
                            (-Config.platformStartSpawnRelativeY) : 
                            (Config.platformStartSpawnRelativeY)) + MainObjects.Player.transform.position.y),
                        Quaternion.identity);

                    checkBox.transform.parent = MainObjects.GameObjectsParent.transform;

                    checkBox.SendMessage("StartChecking");

                    for (int f = 0; f < 10; f++)
                    {
                        checkBox.transform.position = new Vector3(
                            checkBox.transform.position.x + 0.04f,
                            checkBox.transform.position.y,
                            checkBox.transform.position.z
                            );
                        yield return new WaitForSeconds(0.01f);
                    }

                    if (checkBox.tag == "-")
                        SpawnPlatform(lineIndex, CountYStartPlace(lineIndex));

                    Destroy(checkBox);

                    //StoppingTrapSpawningObject.SendMessage("ControlTraps", lineIndex);
                    
                }

                yield return new WaitForSeconds(Config.spawningGameCycleTimeOnGameOffOrOn);
            }
            yield return new WaitForSeconds(Config.spawningGameCycleTimeOnGameOff);
        }
    }
}