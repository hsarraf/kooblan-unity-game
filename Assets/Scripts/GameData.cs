using System;
using UnityEditor;
using UnityEngine;

[Serializable]
public class GameData : PlayerPrefs
{
    public void Init()
    {
        //Debug.Log(Initialized);
        if (Initialized == 0)
        {
            Initialized = 1;
            LevelNum = 1;
            SFX = 1;
            Music = 1;
            TrainingDone = 0;
        }
        else
        {
            _initialized = Initialized;
            _levelNum = LevelNum;
            _sfx = SFX;
            _music = Music;
            _trainingDone = TrainingDone;
        }
    }

    //[MenuItem("mine/clear data", false)]
    static void Clear()
    {
        DeleteAll();
    }

    public int _initialized;
    public int Initialized
    {
        get { return HasKey(GameConfig.INITIALIZED_KEY) ? 1 : 0; }
        set { _initialized = value; SetInt(GameConfig.INITIALIZED_KEY, value); }
    }

    public int _levelNum;
    public int LevelNum
    {
        get { return GetInt(GameConfig.LEVE_NUML_KEY); }
        set { _levelNum = value; SetInt(GameConfig.LEVE_NUML_KEY, value); }
    }

    public int _sfx;
    public int SFX
    {
        get { return GetInt(GameConfig.SFX_KEY); }
        set { _sfx = value; SetInt(GameConfig.SFX_KEY, value); }
    }

    public int _music;
    public int Music
    {
        get { return GetInt(GameConfig.MUSIC_KEY); }
        set { _music = value; SetInt(GameConfig.MUSIC_KEY, value); }
    }

    public int _trainingDone;
    public int TrainingDone
    {
        get { return GetInt(GameConfig.TRAINING_DONE_KEY); }
        set { _music = value; SetInt(GameConfig.TRAINING_DONE_KEY, value); }
    }

    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }

}