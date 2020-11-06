using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Prefabs : MonoBehaviour
{
    public int _LeftCannonBonusNumber = 0;
    public int _RightCannonBonusNumber = 0;

    public GameObject[] _PlatformExamples;
    public GameObject[] _BonusesExamples;
    public Image _FreezeImage;
    public GameObject _PlatformCheckingBox;
    public GameObject _StoppingTrapSpawningObject;
    public GameObject _Coin;
    public GameObject _LeftArrow;
    public GameObject _RightArrow;
    public GameObject _PlatformConnectors;
    public Sprite[] _FiresUp;
    public Sprite[] _FiresDown;

    //in code

    static public int           LeftCannonBonusNumber;
    static public int           RightCannonBonusNumber;

    static public GameObject[]  PlatformExamples;
    static public GameObject[]  BonusesExamples;
    static public Image         FreezeImage;
    static public GameObject    PlatformCheckingBox;
    static public GameObject    StoppingTrapSpawningObject;
    static public GameObject    Coin;
    static public GameObject    LeftArrow;
    static public GameObject    RightArrow;
    static public GameObject    PlatformConnectors;
    static public Sprite[]      FiresUp;
    static public Sprite[]      FiresDown;

    void Start()
    {
        LeftCannonBonusNumber =       _LeftCannonBonusNumber;
        RightCannonBonusNumber =      _RightCannonBonusNumber;
                                      
        PlatformExamples =            _PlatformExamples;
        BonusesExamples =             _BonusesExamples;
        FreezeImage =                 _FreezeImage;
        PlatformCheckingBox =         _PlatformCheckingBox;
        StoppingTrapSpawningObject =  _StoppingTrapSpawningObject;
        Coin =                        _Coin;
        LeftArrow =                   _LeftArrow;
        RightArrow =                  _RightArrow;
        PlatformConnectors =          _PlatformConnectors;
        FiresUp =                     _FiresUp;
        FiresDown =                   _FiresDown;
    }
}
