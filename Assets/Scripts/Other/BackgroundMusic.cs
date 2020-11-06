using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    AudioSource audioSource;

    public AudioClip InGameMusic;
    public AudioClip InMenuMusic;

    public static GameObject backgroundMusic;

    public enum MusicType
    {
        None,
        InGame,
        InMenu
    }

    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        audioSource.loop = true;
        backgroundMusic = this.gameObject;
        BackgroundMusic.InMenu();
    }

    public void ChangeMusic(MusicType mt)
    {
        if (audioSource.isPlaying) audioSource.Stop();
        bool isPlay = true;

        switch (mt)
        {
            case MusicType.InGame:
                audioSource.clip = InGameMusic;
                break;
            case MusicType.InMenu:
                audioSource.clip = InMenuMusic;
                break;
            case MusicType.None:
                isPlay = false;
                break;
            default:
                audioSource.clip = InMenuMusic;
                break;
        }

        if (isPlay) audioSource.Play();
    }

    public static void InGame()
    {
        backgroundMusic.SendMessage("ChangeMusic", MusicType.InGame);
    }

    public static void InMenu()
    {
        backgroundMusic.SendMessage("ChangeMusic", MusicType.InMenu);
    }

    public static void TurnOff()
    {
        backgroundMusic.SendMessage("ChangeMusic", MusicType.None);
    }
}
