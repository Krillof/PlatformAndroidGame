using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config : MonoBehaviour
{
    public bool _isCharacterImmuneToAllTraps = false;
    public float _accelerationCoefficent = 0.0001f;
    public float _speedMax = 0.7f;
    public float _platformsSpeed = 0.02f;
    public float _lineSpeedDiff = 0.01f;
    private int   _linesAmount = 5;
    public float _linesXStart = -2.1f;
    public float _linesSpaceBetween = 0.02f;
    private float _linesXSize = 1;
    public float _platformStartSpawnRelativeY = 10;
    public float _platformDestroyRelativeY = 15;
    public float _movingGameCycleTime = 0.01f;
    public float _spawningGameCycleTimeOnGameOff = 1f;
    public float _spawningGameCycleTimeOnGameOffOrOn = 0.5f;
    public float _playerJumpSpeed = 0.2f;
    public int   _playerMaxJumpTime = 8;
    public float _cameraMoveStepSize = 0.025f;
    public float _arrowSpeed = 0.2f;
    public float _destroyArrowOnDistanceX = 4f;
    public int _maxAccumulatorValue = 1500;

    static public bool isCharacterImmuneToAllTraps;
    static public float accelerationCoefficent;
    static public float speedMax;
    static public float platformsSpeed;
    static public float lineSpeedDiff;
    static public int   linesAmount;
    static public float linesXStart;
    static public float linesSpaceBetween;
    static public float linesXSize;
    static public float platformStartSpawnRelativeY;
    static public float platformDestroyRelativeY;
    static public float movingGameCycleTime;
    static public float spawningGameCycleTimeOnGameOff;
    static public float spawningGameCycleTimeOnGameOffOrOn;
    static public float playerJumpSpeed;
    static public int   playerMaxJumpTime;
    static public float cameraMoveStepSize;
    static public float arrowSpeed;
    static public float destroyArrowOnDistanceX;
    static public int maxAccumulatorValue = 1500;

    void Start()
    {
        isCharacterImmuneToAllTraps =           _isCharacterImmuneToAllTraps;
        accelerationCoefficent =                _accelerationCoefficent;
        speedMax =                              _speedMax;
        platformsSpeed =                        _platformsSpeed;
        lineSpeedDiff =                         _lineSpeedDiff;
        linesAmount =                           _linesAmount;
        linesXStart =                           _linesXStart;
        linesSpaceBetween =                     _linesSpaceBetween;
        linesXSize =                            _linesXSize;
        platformStartSpawnRelativeY =           _platformStartSpawnRelativeY;
        platformDestroyRelativeY =              _platformDestroyRelativeY;
        movingGameCycleTime =                   _movingGameCycleTime;
        spawningGameCycleTimeOnGameOff =        _spawningGameCycleTimeOnGameOff;
        spawningGameCycleTimeOnGameOffOrOn =    _spawningGameCycleTimeOnGameOffOrOn;
        playerJumpSpeed =                       _playerJumpSpeed;
        playerMaxJumpTime =                     _playerMaxJumpTime;
        cameraMoveStepSize =                    _cameraMoveStepSize;
        arrowSpeed =                            _arrowSpeed;
        destroyArrowOnDistanceX =               _destroyArrowOnDistanceX;
        maxAccumulatorValue =                   _maxAccumulatorValue;
    }
}
