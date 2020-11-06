using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    public GameObject MainCharacterObj;
    
    public GameObject Shield;
    public GameObject CannonDetectorLeft;
    public GameObject CannonDetectorRight;
    public GameObject CannonDetectorTop;
    public GameObject CannonDetectorBottom;

    public static GameObject ShieldSphere;

    public static bool? isMovingUp = null;

    static public float PlatformOffset = 0;

    static public int MoneyCount = 0;
    static public int ignoreTrapsIterations = 0;
    static public int freezeIterations = 0;
    static public int currentJumpDistance = 0;


    static public float vx = 0;
    static public float vy = 0;
    static public bool isMoving = true;
    static public GameObject CurrentPlatformPart = null;
    static public bool startPlatformDetection = false;
    static public GameObject CannonDetectorLeftPublic;
    static public GameObject CannonDetectorRightPublic;
    static public GameObject CannonDetectorTopPublic;
    static public GameObject CannonDetectorBottomPublic;
    static public bool canMoveUp = true;
    static public bool canMoveDown = true;
    static public bool canMoveLeft = true;
    static public bool canMoveRight = true;
    
    static float TeleportX = 0;
    static float TeleportY = 0;
    static bool isTeleport = false;

    static ChangingPlayerTexture.Way way = ChangingPlayerTexture.Way.Down;

    void Start()
    {
        MainObjects.Player.SetActive(false);
        ChangingPlayerTexture.StartJump(way = ChangingPlayerTexture.Way.Down);
        Shield = Instantiate(Shield);
        Shield.transform.parent = MainObjects.GameObjectsParent.transform;
        ShieldSphere = Shield;

        CannonDetectorLeftPublic = Transform.Instantiate(CannonDetectorLeft, CannonDetectorLeft.transform.position, Quaternion.identity);
        CannonDetectorRightPublic = Transform.Instantiate(CannonDetectorRight, CannonDetectorRight.transform.position, Quaternion.identity);
        CannonDetectorTopPublic = Transform.Instantiate(CannonDetectorTop, CannonDetectorTop.transform.position, Quaternion.identity);
        CannonDetectorBottomPublic = Transform.Instantiate(CannonDetectorBottom, CannonDetectorBottom.transform.position, Quaternion.identity);

        CannonDetectorLeftPublic.transform.parent = MainObjects.GameObjectsParent.transform;
        CannonDetectorRightPublic.transform.parent = MainObjects.GameObjectsParent.transform;
        CannonDetectorTopPublic.transform.parent = MainObjects.GameObjectsParent.transform;
        CannonDetectorBottomPublic.transform.parent = MainObjects.GameObjectsParent.transform;

        CannonDetectorLeftPublic.SendMessage("SetType", CannonDetector.Types.left);
        CannonDetectorRightPublic.SendMessage("SetType", CannonDetector.Types.right);
        CannonDetectorTopPublic.SendMessage("SetType", CannonDetector.Types.up);
        CannonDetectorBottomPublic.SendMessage("SetType", CannonDetector.Types.down);
    }

    public static void GameStarted()
    {
        MainObjects.Player.SetActive(true);
        MainObjects.Player.transform.position = new Vector3(
               GameController.CountXPlace(2),
                  1,
               MainObjects.Player.transform.position.z
                );
    }

    void EndGame()
    {
        GameController.EndGame();
        startPlatformDetection = false;
        CurrentPlatformPart = null;
        isMoving = true;
        vx = 0;
        vy = 0;
        MainObjects.Player.SetActive(false);
        currentJumpDistance = 0;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        switch (collider.gameObject.tag)
        {
            case "GameOverObject":
                if (!Config.isCharacterImmuneToAllTraps)
                {
                    EndGame();
                }
                break;
            case "Trap":
                if (!Config.isCharacterImmuneToAllTraps)
                    if (ignoreTrapsIterations == 0)
                        {
                            EndGame();
                        }
                break;
            case "ElectricTrap":
                if (!Config.isCharacterImmuneToAllTraps)
                    if (ignoreTrapsIterations == 0)
                    {
                        EndGame();
                        ChangingPlayerTexture.AnimateElectricDeath();
                    }
                break;
            case "Item":
                if (collider.gameObject.activeSelf)
                {
                    collider.gameObject.SendMessage("GetItem");
                    collider.gameObject.SetActive(false);
                }
                break;
            case "PlatformPart":
                ChangingPlayerTexture.EndJump(way);
                startPlatformDetection = true;
                CurrentPlatformPart = collider.gameObject;
                CurrentPlatformPart.SendMessage("GetOffsetToPlayer");
                currentJumpDistance = 0;
                isMoving = false;
                break;
            case "Money":
                MoneyCount++;
                MainObjects.TopPad.SendMessage("SetMoneyText", "Money: " + MoneyCount);
                Destroy(collider.gameObject);
                break;
        }
    }

    static float CountStep(GameObject RelativeTo)
    {
        if (Math.Abs(MainObjects.Player.transform.position.y - RelativeTo.transform.position.y) > 0.1f)
            return (MainObjects.Player.transform.position.y - RelativeTo.transform.position.y) / 10;
        else
            return 0;
    }

    public static void Teleport(float x, float y)
    {
        TeleportX = x;
        TeleportY = y;
        isTeleport = true;
    }

    void MoveDetectors()
    {
        CannonDetectorLeftPublic.transform.position = new Vector3(MainObjects.Player.transform.position.x - 0.5f,
                MainObjects.Player.transform.position.y,
                MainObjects.Player.transform.position.z);
        CannonDetectorRightPublic.transform.position = new Vector3(MainObjects.Player.transform.position.x + 0.5f,
            MainObjects.Player.transform.position.y,
            MainObjects.Player.transform.position.z);
        CannonDetectorTopPublic.transform.position = new Vector3(MainObjects.Player.transform.position.x,
            MainObjects.Player.transform.position.y + 0.5f,
            MainObjects.Player.transform.position.z);
        CannonDetectorBottomPublic.transform.position = new Vector3(MainObjects.Player.transform.position.x,
            MainObjects.Player.transform.position.y - 0.5f,
            MainObjects.Player.transform.position.z);
    }

    public void Move()
    {
        if (ignoreTrapsIterations > 0)
        {
            ShieldSphere.SetActive(true);
        } else
        {
            ShieldSphere.SetActive(false);
        }


        if (isMoving)
        {
            MainObjects.Player.transform.position = new Vector3(
            MainObjects.Player.transform.position.x + vx,
            MainObjects.Player.transform.position.y +
                vy + ((vy > 0) ? (1) : (-1)) * MainObjects.Player.transform.position.y * Config.accelerationCoefficent,
            MainObjects.Player.transform.position.z);

            MoveDetectors();


            if (startPlatformDetection)
                currentJumpDistance++;

            if (currentJumpDistance == Config.playerMaxJumpTime)
            {

                EndGame();
                return;
            }

            ChangingPlayerTexture.Jump(way);
        }
        else
        {
            if (isTeleport)
            {
                CurrentPlatformPart = null;
                MainObjects.Player.transform.position
                    = new Vector3(TeleportX, TeleportY, MainObjects.Player.transform.position.z);

                MoveDetectors();

                MainObjects.Player.transform.rotation = new Quaternion(0, 0, 0, 0);

                isTeleport = false;
            }
            else if (CurrentPlatformPart != null)
            {

                MainObjects.Player.transform.position = new Vector3(
                      MainCharacter.CurrentPlatformPart.transform.position.x,
                      MainCharacter.CurrentPlatformPart.transform.position.y + PlatformOffset,
                      MainCharacter.CurrentPlatformPart.transform.position.z
                );

                MoveDetectors();
            }
        }

        MoveCamera();
    }

    public static void MoveCamera()
    {

        CameraBehavior.CameraObject.transform.position = new Vector3(
            CameraBehavior.CameraObject.transform.position.x,
            CameraBehavior.CameraObject.transform.position.y + CountStep(CameraBehavior.CameraObject),
            CameraBehavior.CameraObject.transform.position.z);

        MainObjects.GameCanvas.transform.position = new Vector3(
            CameraBehavior.CameraObject.transform.position.x,
            MainObjects.GameCanvas.transform.position.y + CountStep(MainObjects.GameCanvas),
            90);

        MainObjects.StaticCanvas.transform.position = new Vector3(
           CameraBehavior.CameraObject.transform.position.x,
           MainObjects.StaticCanvas.transform.position.y + CountStep(MainObjects.StaticCanvas),
           90);

        ShieldSphere.transform.position = new Vector3(
            MainObjects.Player.transform.position.x,
            MainObjects.Player.transform.position.y
            );

        BackgroundController.Move();
    }

    public static void CharacterCommand(string cmd)
    {
        if (!startPlatformDetection || isMoving) return;


        switch (cmd)
        {
            case "Up":

                if (!canMoveUp) return;

                vy = Config.playerJumpSpeed;
                vx = 0;


                isMoving = true;
                ChangingPlayerTexture.StartJump(way = ChangingPlayerTexture.Way.Up);

                break;
            case "Down":

                if (!canMoveDown) return;

                vy = -Config.playerJumpSpeed;
                vx = 0;

                isMoving = true;
                ChangingPlayerTexture.StartJump(way = ChangingPlayerTexture.Way.Down);
                break;
            case "Left":

                if (!canMoveLeft) return;

                vy = 0;
                vx = -Config.playerJumpSpeed;


                isMoving = true;
                ChangingPlayerTexture.StartJump(way = ChangingPlayerTexture.Way.Left);
                break;
            case "Right":

                if (!canMoveRight) return;

                vy = 0;
                vx = Config.playerJumpSpeed;


                isMoving = true;
                ChangingPlayerTexture.StartJump(way = ChangingPlayerTexture.Way.Right);
                break;
        }
        CurrentPlatformPart = null;
    }
}
