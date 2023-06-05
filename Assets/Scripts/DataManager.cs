using UnityEngine;

public class DataManager : MonoBehaviour
{
    private static DataManager __instance;
    public static DataManager Instance { get { return __instance; } }
    void Awake()
    {
        if (__instance == null)
        {
            __instance = this;
            DontDestroyOnLoad(gameObject);

            _gameData.Init();
            LoadData();
        }
        else if (__instance != this)
        {
            Destroy(gameObject);
        }
    }

    public GameData _gameData;

    public Texture2D[] _levelePatterns;
    public Texture2D[] _greyPatterns;
    public Texture2D CurrentLevelPattern { get { return _levelePatterns[LevelNum - 1]; } }
    public Texture2D CurrentGreyPattern { get { return _greyPatterns[LevelNum - 1]; } }

    void LoadData()
    {
        _levelNum = _gameData.LevelNum;
        _music = _gameData.Music;
        _sfx = _gameData.SFX;
    }

    [Header("[User]")]
    int _levelNum;
    public int LevelNum
    {
        get { return _levelNum; }
        set { _gameData.LevelNum = _levelNum = value; }
    }

    [Header("[Sound]")]
    int _music;
    public int Music
    {
        get { return _music; }
        set { _gameData.Music = _music = value; }
    }
    int _sfx;
    public int SFX
    {
        get { return _sfx; }
        set { _gameData.SFX = _sfx = value; }
    }

    [Header("[Training]")]
    int _trainingDone;
    public int TrainingDone
    {
        get { return _trainingDone; }
        set { _gameData.TrainingDone = _trainingDone = value; }
    }

}
