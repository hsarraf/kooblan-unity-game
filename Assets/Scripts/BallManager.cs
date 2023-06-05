using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    private static BallManager _instance;
    public static BallManager Instance { get { return _instance; } }
    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;

            _ballDict = new Dictionary<Ball.BallType, Sprite>();
            _ballDict[Ball.BallType.none] = _none;
            _ballDict[Ball.BallType.solid] = _solid;
            _ballDict[Ball.BallType.red] = _red;
            _ballDict[Ball.BallType.green] = _green;
            _ballDict[Ball.BallType.blue] = _blue;
            _ballDict[Ball.BallType.cyan] = _cyan;
            _ballDict[Ball.BallType.magenta] = _magenta;
            _ballDict[Ball.BallType.yellow] = _yellow;
        }
    }

    public Sprite _none;
    public Sprite _solid;
    public Sprite _red;
    public Sprite _green;
    public Sprite _blue;
    public Sprite _cyan;
    public Sprite _magenta;
    public Sprite _yellow;

    public Dictionary<Ball.BallType, Sprite> _ballDict = null;

    public bool _finished = false;
    public Ball.BallType _targetColor;

    public bool _gameover = false;

    public enum InfectionType
    {
        dfs, bfs
    }

    internal void SetBallsDirty(Ball.BallType ballType)
    {
        for (int i = 0; i < LevelManager.Instance._xCount; i++)
        {
            for (int j = 0; j < LevelManager.Instance._yCount; j++)
            {
                Ball ball = LevelManager.Instance._ballsMatrix[i][j];
                if (ball._type == ballType || ball._type == Ball.BallType.solid)
                    ball._dirty = true;
                else
                    ball._dirty = false;
            }
        }
    }

    public List<Ball> DFS(Ball from)
    {
        //bool randDFS = Random.value < 0.5f;
        List<Ball> dfs = new List<Ball>();
        Stack<Ball> stack = new Stack<Ball>();
        stack.Push(from);
        while (stack.Count > 0)
        {
            from = stack.Pop(); 
            if (from._dirty)
            {
                from._dirty = false;
                dfs.Add(from);
                //if (randDFS)
                //{
                //for (int j = 0, i = Random.Range(0, from._nextList.Count); j < 4; i = (i + 1) % from._nextList.Count, j++)
                //    stack.Push(from._nextList[i]);
                //}
                //else
                //{
                for (int j = 0, i = Random.Range(0, from._nextList.Count); j < 4; i = (i + 1) % from._nextList.Count, j++)
                    stack.Push(from._nextList[i]);
                //foreach (Ball adj in from._nextList)
                //    stack.Push(adj);
                //}
            }
        }
        return dfs;
    }

    public List<Ball> BFS(Ball from)
    {
        List<Ball> bfs = new List<Ball>();
        Queue<Ball> queue = new Queue<Ball>();
        from._dirty = false;
        queue.Enqueue(from);
        bfs.Add(from);
        while (queue.Count > 0)
        {
            from = queue.Dequeue();
            foreach (Ball adj in from._nextList)
            {
                if (adj._dirty)
                {
                    adj._dirty = false;
                    queue.Enqueue(adj);
                    bfs.Add(adj);
                }
            }
        }
        return bfs;
    }

    internal void InfectAll(Ball from, float initSpeed, int infectFactor, float end, InfectionType infectionType)
    {
        for (int i = 0; i < LevelManager.Instance._xCount; i++)
            for (int j = 0; j < LevelManager.Instance._yCount; j++)
                LevelManager.Instance._ballsMatrix[i][j]._dirty = true;
        StartCoroutine(InfectCo(infectionType == InfectionType.dfs ? DFS(from) : BFS(from), initSpeed, infectFactor, end, true));
    }

    internal void Infect(Ball from, float initSpeed, int infectFactor, float end, InfectionType infectionType = InfectionType.dfs, bool gameIsOver = false)
    {
        SetBallsDirty(CanvasHUD.Instance._tappedColor);
        StartCoroutine(InfectCo(infectionType == InfectionType.dfs ? DFS(from) : BFS(from), initSpeed, infectFactor, end, gameIsOver));
    }

    public IEnumerator InfectCo(List<Ball> balls, float initSpeed, int infectFactor, float end, bool gameIsOver)
    {
        int s = 0;
        int count = balls.Count;
        foreach (Ball ball in balls)
        {
            ball.Type = _targetColor;
            ball._trigger = true;
            ball._fxPart1.Play();
            s += infectFactor;

            float grey = LevelManager.Instance._greyPattern.GetPixel(LevelManager.Instance._tileWidth * ball._i, LevelManager.Instance._tileHeight * ball._j).grayscale;
            grey = Mathf.Lerp(0.4f, 0.9f, Mathf.FloorToInt(grey * 10f) / 10f);
            ball.SetGrey(grey);

            float p = Mathf.Lerp(initSpeed, end, (float)s / count);
            if (Random.value > p)
                yield return null;
        }
        if (!gameIsOver)
        {
            CheckGameOver();
            CanvasHUD.Instance.Activate();
        }
    }


    internal void InfectAllColored(Ball from, float initSpeed, int infectFactor, float end, InfectionType infectionType)
    {
        for (int i = 0; i < LevelManager.Instance._xCount; i++)
            for (int j = 0; j < LevelManager.Instance._yCount; j++)
                LevelManager.Instance._ballsMatrix[i][j]._dirty = true;
        StartCoroutine(InfectAllColoredCo(infectionType == InfectionType.dfs ? DFS(from) : BFS(from), initSpeed, infectFactor, end));
    }
    public IEnumerator InfectAllColoredCo(List<Ball> balls, float initSpeed, int infectFactor, float end)
    {
        int s = 0;
        int count = balls.Count;
        foreach (Ball ball in balls)
        {
            ball.Type = _targetColor;
            ball._trigger = true;
            ball._fxPart1.Play();
            s += infectFactor;

            Color grey = LevelManager.Instance._greyPattern.GetPixel(LevelManager.Instance._tileWidth * ball._i, LevelManager.Instance._tileHeight * ball._j);
            //grey = Mathf.Lerp(0.4f, 0.9f, Mathf.FloorToInt(grey * 10f) / 10f);
            ball.SetGrey(grey);

            float p = Mathf.Lerp(initSpeed, end, (float)s / count);
            if (Random.value > p)
                yield return null;
        }
    }

    public void CheckGameOver()
    {
        bool fin = true;
        for (int i = 0; i < LevelManager.Instance._xCount; i++)
            for (int j = 0; j < LevelManager.Instance._yCount; j++)
                fin &= (LevelManager.Instance._ballsMatrix[i][j]._type == _targetColor || 
                    LevelManager.Instance._ballsMatrix[i][j]._type == Ball.BallType.solid);
        if (fin)
        {
            GameOver(true);
            _gameover = true;
        }
        else if (LevelManager.Instance.SelectLimit == 1)
        {
            CanvasMessage.Instance.ShowMessage("Last Chance");
        }
        else if (LevelManager.Instance.SelectLimit == 0)
        {
            GameOver(false);
            _gameover = true;
        }
    }

    public void GameOver(bool? win)
    {
        GameManager.Instance.GameOver(win);
        StopAllCoroutines();
        _finished = true;
        StartCoroutine(GameOverCo(win));
    }

    IEnumerator GameOverCo(bool? win)
    {
        // flood all tiles with greyscale picture
        //
        CanvasHUD.Instance._tappedColor = _targetColor;
        if (win == true)
        {
            _targetColor = Ball.BallType.solid;
            InfectAll(LevelManager.Instance.CenterBall, 0.5f, 65, 0.99f, InfectionType.dfs);
        }
        else if (win == false)
        {
            _targetColor = Ball.BallType.none;
            InfectAll(LevelManager.Instance.CenterBall, 0.5f, 65, 0.99f, InfectionType.bfs);
            CanvasLose.Instance.Show();
            yield break;
        }
        else
        {
            _targetColor = Ball.BallType.none;
            InfectAll(LevelManager.Instance._ballsMatrix[0][0], 0.5f, 65, 0.99f, InfectionType.bfs);
            CanvasTryMore.Instance.Show();
            yield break;
        }

        yield return new WaitForSeconds(1.5f);

        // flood all tiles with colored picture
        //
        _targetColor = Ball.BallType.solid;
        InfectAllColored(LevelManager.Instance._ballsMatrix[0][0], 0.5f, 50, 0.99f, InfectionType.bfs);

        yield return new WaitForSeconds(1.5f);

        // show win/lose/trymore panel
        //
        if (win == true)
            CanvasWin.Instance.Show();

        CanvasHUD.Instance.Hide();
    }

}
