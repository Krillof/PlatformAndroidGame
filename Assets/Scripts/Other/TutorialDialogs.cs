using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDialogs : MonoBehaviour
{
    public GameObject UnstopButton;
    private GameObject DialogPicture = null;

    public Sprite PlayerDialog;

    public void Unstop()
    {
        GameController.isGameStop = false;
        UnstopButton.SetActive(false);
        DialogPicture.SetActive(false);
        InfoSaver.SavedData.isSeenPlayer = true;
    }

    public void MakeDialogPicture()
    {
        DialogPicture = new GameObject();
        DialogPicture.transform.parent = MainObjects.TutorialDialogs.transform;
        DialogPicture.AddComponent<SpriteRenderer>();
        DialogPicture.transform.localScale = new Vector3(1.4f, 1.4f);
        DialogPicture.GetComponent<SpriteRenderer>().sortingOrder = 0;
        DialogPicture.SetActive(false);
    }

    public void ShowPlayerDialog()
    {
        GameController.isGameStop = true;
        UnstopButton.SetActive(true);

        if (DialogPicture == null) MakeDialogPicture();

        DialogPicture.GetComponent<SpriteRenderer>().sprite = PlayerDialog;
        DialogPicture.transform.position = new Vector3(
            MainObjects.Player.transform.position.x,
            MainObjects.Player.transform.position.y+2,
            MainObjects.Player.transform.position.z
            );
        DialogPicture.SetActive(true);
    }


}
