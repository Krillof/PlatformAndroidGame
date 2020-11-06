using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BackgroundController : MonoBehaviour
{
    public GameObject[] BackgroundLayersA;
    public GameObject[] BackgroundLayersB;

    private static BackgroundController BGController = null;

    private float[] SpeedCoefficients = { 0.9f, 0.75f, 0.65f };
    private float bottomRange = 5.5f;
    private float topRange = 5.5f;
    private float size = 26f;

    private int[] KA = { 0, 0, 0 };
    private int[] KB = { 0, 0, 0 };


    void Start()
    {
        BGController = this;
    }

    void _Move()
    {
        for (int f = 0; f < BackgroundLayersA.Length; f++)
        {
            float deltaY_A = CameraBehavior.CameraObject.transform.position.y - BackgroundLayersA[f].transform.position.y;
            float deltaY_B = CameraBehavior.CameraObject.transform.position.y - BackgroundLayersB[f].transform.position.y;

            int ka = 1;
            int kb = 1;

            if (deltaY_A < 0)
            {
                ka = -1;
                deltaY_A = -deltaY_A;
                deltaY_A += bottomRange;
            }
            else
            {
                deltaY_A += topRange;
            }

            if (deltaY_B < 0)
            {
                kb = -1;
                deltaY_B = -deltaY_B;
                deltaY_B += bottomRange;
            }
            else
            {
                deltaY_B += topRange;
            }

            if ((deltaY_A > size * 3 / 2) && (deltaY_B > 0)) KA[f] += ka;
            if ((deltaY_B > size * 3 / 2) && (deltaY_A > 0)) KB[f] += kb;



            BackgroundLayersA[f].transform.position = new Vector3(
                CameraBehavior.CameraObject.transform.position.x,
                CameraBehavior.CameraObject.transform.position.y * SpeedCoefficients[f] + size * 2 * KA[f]
                );

            BackgroundLayersB[f].transform.position = new Vector3(
                CameraBehavior.CameraObject.transform.position.x,
                CameraBehavior.CameraObject.transform.position.y * SpeedCoefficients[f] + size * 2 * KB[f] + size
                );
        }
    }

    public static void Move()
    {
        BGController.SendMessage("_Move");
    }
}
