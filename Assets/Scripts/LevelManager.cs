using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    private static LevelManager _instance;
    public static LevelManager Instance { get { return _instance; } }
    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;

            _levelPattern = DataManager.Instance.CurrentLevelPattern;
            _greyPattern = DataManager.Instance.CurrentGreyPattern;
            _xCount = _levelPattern.width / _tileWidth + 1;
            _yCount = _levelPattern.height / _tileHeight + 1;
            _ballsMatrix = new Ball[_xCount][];
            for (int i = 0; i < _xCount; i++)
                _ballsMatrix[i] = new Ball[_yCount];
        }
    }

    public Texture2D _levelPattern;
    public Texture2D _greyPattern;

    public Ball _ballPrefab;

    public int _tileWidth;
    public int _tileHeight;

    Color _pixel;
    float _grey;
    float _px;
    float _py;

    internal Ball[][] _ballsMatrix;
    internal int _xCount;
    internal int _yCount;

    public Ball CenterBall { get { return _ballsMatrix[_xCount / 2][_yCount / 2]; } }

    internal bool _generated = false;

    public int _selectLimit;

    public TrainingHandler _trainingHandler;

    public HashSet<Ball.BallType> _ballTypeSet;
    public int SelectLimit
    {
        get { return _selectLimit; }
        set
        {
            _selectLimit = CanvasHUD.Instance.SelectLimitTxt = value;
        }
    }

    bool IsColor(Color input, Color toColor)
    {
        float d = 0.1f;
        bool r = input.r < toColor.r + d && input.r > toColor.r - d;
        bool g = input.g < toColor.g + d && input.g > toColor.g - d;
        bool b = input.b < toColor.b + d && input.b > toColor.b - d;
        return r && g && b;
    }

    void Start()
    {
        _ballTypeSet = new HashSet<Ball.BallType>();
        // make matrix
        //
        SelectLimit = int.Parse(DataManager.Instance.CurrentLevelPattern.name.Split('-')[1]);
        Camera camera = CameraHandler.Instance._camera;
        float camSize = camera.orthographicSize;
        float camRatio = camera.aspect;
        float x = Mathf.Lerp(0.04f, 0.057f, Mathf.InverseLerp(0.5f, 0.75f, camRatio));
        float y = Mathf.Lerp(0.7f, 0.65f, Mathf.InverseLerp(0.5f, 0.75f, camRatio));
        float levelWidth = _levelPattern.width;
        float levelHeight = _levelPattern.height;
        for (int i = 0; i < _xCount; i++)
        {
            //Color c = Color.white * Random.Range(0.85f, 0.93f);
            for (int j = 0; j < _yCount; j++)
            {
                _px = Mathf.Lerp(-camSize * camRatio, camSize * camRatio, _tileWidth * i / levelWidth) + x;// + (j % 2 == 0 ? 0.03f : -0.03f);
                _py = Mathf.Lerp(-camSize * y, camSize * 1f, _tileHeight * j / levelHeight) - 0.55f;
                _pixel = _levelPattern.GetPixel(_tileWidth * i, _tileHeight * j);
                Ball ball = Instantiate(_ballPrefab, new Vector2(_px, _py), Quaternion.Euler(0f, 0f, 45f));
                ball._transform.SetParent(transform);
                ball.SetIndex(i, j);
                if (IsColor(_pixel, Color.red))
                    ball.Type = Ball.BallType.red;
                else if (IsColor(_pixel, Color.green))
                    ball.Type = Ball.BallType.green;
                else if (IsColor(_pixel, Color.blue))
                    ball.Type = Ball.BallType.blue;
                else if (IsColor(_pixel, Color.cyan))
                    ball.Type = Ball.BallType.cyan;
                else if (IsColor(_pixel, Color.magenta))
                    ball.Type = Ball.BallType.magenta;
                else if (IsColor(_pixel, Color.yellow))
                    ball.Type = Ball.BallType.yellow;
                else
                {
                    if (i > 0 && _ballsMatrix[i - 1][j]._type != Ball.BallType.none)
                        ball.Type = _ballsMatrix[i - 1][j]._type;
                    else if (j > 0)
                        ball.Type = _ballsMatrix[i][j - 1]._type;
                    //ball.Type = Ball.BallType.solid;
                }
                ball._renderer.color = Color.white * Random.Range(0.9f, 0.95f);
                //ball._renderer.color = Color.white * (Mathf.PerlinNoise((float)i / _xCount * 8f, (float)j / _yCount * 8f) * 0.075f + 0.85f);

                //_grey = _greyPattern.GetPixel(_tileWidth * i, _tileHeight * j).grayscale;
                //_grey = Mathf.Lerp(0.4f, 0.9f, Mathf.FloorToInt(_grey * 10f) / 10f);
                //ball._renderer.color = Color.white * _grey;
                //ball.SetGrey(_grey);

                _ballsMatrix[i][j] = ball;
                if (ball._type != Ball.BallType.none && ball._type != Ball.BallType.solid)
                    _ballTypeSet.Add(ball._type);
            }
        }

        // make linked list
        //
        for (int i = 0; i < _xCount - 1; i++)
            for (int j = 0; j < _yCount; j++)
                _ballsMatrix[i][j]._nextList.Add(_ballsMatrix[i + 1][j]);
        for (int i = _xCount - 1; i > 0; i--)
            for (int j = 0; j < _yCount; j++)
                _ballsMatrix[i][j]._nextList.Add(_ballsMatrix[i - 1][j]);
        for (int j = 0; j < _yCount - 1; j++)
            for (int i = 0; i < _xCount; i++)
                _ballsMatrix[i][j]._nextList.Add(_ballsMatrix[i][j + 1]);
        for (int j = _yCount - 1; j > 0; j--)
            for (int i = 0; i < _xCount; i++)
                _ballsMatrix[i][j]._nextList.Add(_ballsMatrix[i][j - 1]);

        _generated = true;
    }

    //Texture2D Reformat(Texture2D texture2D)
    //{
    //    int targetX = 1024;
    //    int targetY = (int)(targetX / CameraHandler.Instance._camera.aspect * 0.65f);
    //    RenderTexture rt = new RenderTexture(targetX, targetY, 24);
    //    RenderTexture.active = rt;
    //    Graphics.Blit(texture2D, rt);
    //    Texture2D result = new Texture2D(targetX, targetY);
    //    result.ReadPixels(new Rect(0, 0, targetX, targetY), 0, 0);
    //    result.Apply();
    //    return result;
    //}

}
