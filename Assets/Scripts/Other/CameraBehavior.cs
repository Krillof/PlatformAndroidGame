using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public GameObject Camera;
    public static GameObject CameraObject;

    // Start is called before the first frame update
    void Start()
    {
        CameraObject = Camera;
    }
}
