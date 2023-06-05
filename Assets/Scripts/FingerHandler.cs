using UnityEngine;

public class FingerHandler : MonoBehaviour
{

    private static FingerHandler _instance;
    public static FingerHandler Instance { get { return _instance; } }
    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            _transform = GetComponent<Transform>();
            _collider = GetComponent<CircleCollider2D>();
        }
    }

    Transform _transform;
    CircleCollider2D _collider;

    void Update()
    {
        if (!CanvasHUD.Instance._UIActivated && CanvasHUD.Instance._active && !BallManager.Instance._gameover)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _trigger = false;
                _transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                _collider.radius = 0.1f;
            }
            else
            {
                _collider.radius = Mathf.Lerp(_collider.radius, 0f, Time.deltaTime);
                if (_collider.radius < 0.05f)
                    _transform.position = Vector2.one * -1000f;
            }
        }
    }

    bool _trigger = false;
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (_trigger || CanvasHUD.Instance._selectedColor == Ball.BallType.solid || CanvasHUD.Instance._selectedColor == Ball.BallType.none)
            return;
        _trigger = true;
        Ball ball = collider.GetComponent<Ball>();
        BallManager.Instance._targetColor = CanvasHUD.Instance._selectedColor;
        if (ball._type != BallManager.Instance._targetColor)
        {
            CanvasHUD.Instance.Deactivate();
            CanvasHUD.Instance._tappedColor = ball._type;
            BallManager.Instance.Infect(ball, 0.5f, 7, 0.97f, BallManager.InfectionType.dfs);
            LevelManager.Instance.SelectLimit--;
            LevelManager.Instance._trainingHandler.Hide();
        }
        else
        {
            ball._trigger = true;
        }
    }

}
