using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ChangingPlayerTexture : MonoBehaviour
{
    static GameObject thisObject;

    static GameObject DeadCharacter;
    static SpriteRenderer DeadCharacterSR;

    public Sprite[] Left;
    public Sprite[] Down;
    public Sprite[] Right;
    public Sprite[] Up;

    public Sprite[] ElectricDeath;

    static public Sprite[] LeftPublic;
    static public Sprite[] DownPublic;
    static public Sprite[] RightPublic;
    static public Sprite[] UpPublic;

    static Sprite[] ElectricDeathPublic;

    static Sprite[] PointerToDeathArrayAnimation;

    void Start()
    {
        DeadCharacter = new GameObject();
        DeadCharacterSR = DeadCharacter.AddComponent<SpriteRenderer>();
        DeadCharacterSR.sortingOrder = 10;
        DeadCharacter.SetActive(false);

        thisObject = this.gameObject;
        LeftPublic = Left;
        RightPublic = Right;
        DownPublic = Down;
        UpPublic = Up;
        ElectricDeathPublic = ElectricDeath;
    }

    public static void StartJump(Way whichWay)
    {
        SpriteRenderer sr = MainObjects.Player.GetComponent<SpriteRenderer>();

        switch (whichWay)
        {
            case Way.Up:
                sr.sprite = UpPublic[0];
                break;
            case Way.Down:
                sr.sprite = DownPublic[0];
                break;
            case Way.Left:
                sr.sprite = LeftPublic[0];
                break;
            case Way.Right:
                sr.sprite = RightPublic[0];
                break;
        }
    }

    public static void Jump(Way whichWay)
    {
        SpriteRenderer sr = MainObjects.Player.GetComponent<SpriteRenderer>();

        switch (whichWay)
        {
            case Way.Up:
                sr.sprite = UpPublic[1];
                break;
            case Way.Down:
                sr.sprite = DownPublic[1];
                break;
            case Way.Left:
                sr.sprite = LeftPublic[1];
                break;
            case Way.Right:
                sr.sprite = RightPublic[1];
                break;
        }
    }

    public static void EndJump(Way whichWay)
    {
        SpriteRenderer sr = MainObjects.Player.GetComponent<SpriteRenderer>();

        switch (whichWay)
        {
            case Way.Up:
                sr.sprite = UpPublic[2];
                break;
            case Way.Down:
                sr.sprite = DownPublic[2];
                break;
            case Way.Left:
                sr.sprite = LeftPublic[2];
                break;
            case Way.Right:
                sr.sprite = RightPublic[2];
                break;
        }
    }

    public static void AnimateElectricDeath()
    {
        PointerToDeathArrayAnimation = ElectricDeathPublic;
        thisObject.SendMessage("StartDeathAnimation");
    }

    public void StartDeathAnimation()
    {
        StartCoroutine(DeathAnimation());
    }

    IEnumerator DeathAnimation()
    {
        DeadCharacter.SetActive(true);
        DeadCharacter.transform.position = MainObjects.Player.transform.position;

        for (int f = 0; f < 10; f++)
        {
            DeadCharacterSR.sprite = PointerToDeathArrayAnimation[f % PointerToDeathArrayAnimation.Length];
            yield return new WaitForSeconds(0.2f);
        }

        DeadCharacter.SetActive(false);
    }

    public enum Way
    {
        Up,
        Down,
        Left,
        Right
    }
}
