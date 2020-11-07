using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCanvasBehavior : MonoBehaviour
{
    public GameObject topPart;
    public GameObject bottomPart;
    public GameObject gameButton;
    public GameObject pad;

    Vector3 startPointTopPart;
    Vector3 bottomPointBottomPart;


    void Start()
    {
        startPointTopPart = topPart.transform.position;
        bottomPointBottomPart = bottomPart.transform.position;
    }

    IEnumerator OpenDoor()
    {
        for (int f = 0; f < 11; f++)
        {
            topPart.transform.position = new Vector3(
                topPart.transform.position.x,
                topPart.transform.position.y + f * 0.1f,
                topPart.transform.position.z
                );

            bottomPart.transform.position = new Vector3(
                 bottomPart.transform.position.x,
                 bottomPart.transform.position.y - f * 0.1f,
                 bottomPart.transform.position.z
                );

            yield return new WaitForSeconds(0.05f);
        }

        topPart.SetActive(false);
        bottomPart.SetActive(false);
    }

    IEnumerator CloseDoor()
    {
        topPart.SetActive(true);
        bottomPart.SetActive(true);

        for (int f = 0; f < 11; f++)
        {
            topPart.transform.position = new Vector3(
                topPart.transform.position.x,
                topPart.transform.position.y - f * 0.1f,
                topPart.transform.position.z
                );

            bottomPart.transform.position = new Vector3(
                 bottomPart.transform.position.x,
                 bottomPart.transform.position.y + f * 0.1f,
                 bottomPart.transform.position.z
                );

            yield return new WaitForSeconds(0.05f);
        }

        pad.SetActive(true);
        gameButton.SetActive(true);
    }

    void StartGame()
    {
        gameButton.SetActive(false);
        pad.SetActive(false);
        StartCoroutine(OpenDoor());
    }

    void EndGame()
    {
        StartCoroutine(CloseDoor());
    }

}
