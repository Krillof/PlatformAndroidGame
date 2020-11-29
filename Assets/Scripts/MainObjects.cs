using UnityEngine;

public class MainObjects : MonoBehaviour
{
    public GameObject _Player;
    public GameObject _Camera;
    public GameObject _GameController;
    public GameObject _StatsController;
    public GameObject _GameCanvas;
    public GameObject _MenuCanvas;
    public GameObject _StaticCanvas;
    public GameObject _TopPad;
    public GameObject _GameObjectsParent;
    public GameObject _TutorialDialogs;

    static public GameObject Player;
    static public GameObject Camera;
    static public GameObject GameController;
    static public GameObject StatsController;
    static public GameObject GameCanvas;
    static public GameObject MenuCanvas;
    static public GameObject StaticCanvas;
    static public GameObject TopPad;
    static public GameObject GameObjectsParent;
    static public GameObject TutorialDialogs;

    void Start()
    {
        Player = _Player;
        Camera = _Camera;
        GameController = _GameController;
        StatsController = _StatsController;
        GameCanvas = _GameCanvas;
        MenuCanvas = _MenuCanvas;
        StaticCanvas = _StaticCanvas;
        TopPad = _TopPad;
        GameObjectsParent = _GameObjectsParent;
        TutorialDialogs = _TutorialDialogs;
    }
}
