using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreTeleportCheck : MonoBehaviour
{
    public static bool isGoodPlace = false;

    public GameObject checker;
    public static GameObject checkerPublic;
    public static PreTeleportCheck thisObject;

    public static float X;
    public static float Y;

    void Start()
    {
        checkerPublic = checker;
        thisObject = this;
    }

    public static void CheckPlace(float x, float y)
    {
        X = x;
        Y = y;
        thisObject.SendMessage("Check_Place");
    }

    public void Check_Place()
    {
        StartCoroutine(Check__Place());
    }

    IEnumerator Check__Place()
    {
        GameObject chObj = Instantiate(checkerPublic, new Vector3(X - 0.5f, Y, 0), Quaternion.identity);

        for (int f = -5; f < 5; f++)
        {
            chObj.transform.position = new Vector3(X + f * 0.1f, Y);
            yield return new WaitForSeconds(0.005f);
        }

        Destroy(chObj);
    }
}
