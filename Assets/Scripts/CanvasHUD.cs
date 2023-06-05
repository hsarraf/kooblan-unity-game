using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasHUD : MonoBehaviour
{

    private static CanvasHUD _instance;
    public static CanvasHUD Instance { get { return _instance; } }
    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;

            _colorBttnDict = new Dictionary<Ball.BallType, Button>();
            _colorBttnDict[Ball.BallType.red] = _redBttn;
            _colorBttnDict[Ball.BallType.green] = _greenBttn;
            _colorBttnDict[Ball.BallType.blue] = _blueBttn;
            _colorBttnDict[Ball.BallType.cyan] = _cyanBttn;
            _colorBttnDict[Ball.BallType.magenta] = _magentaBttn;
            _colorBttnDict[Ball.BallType.yellow] = _yellowBttn;
        }
    }

    public Button _redBttn;
    public Button _greenBttn;
    public Button _blueBttn;
    public Button _cyanBttn;
    public Button _magentaBttn;
    public Button _yellowBttn;
    public Dictionary<Ball.BallType, Button> _colorBttnDict;

    public List<Button> _colorBttnList;

    public RectTransform _frameTrs;

    public Ball.BallType _selectedColor;
    public Ball.BallType _tappedColor;

    internal bool _UIActivated = false;

    public bool _active;

    public Text _levelNumTxt;
    public int LevelNumTxt { set { _levelNumTxt.text = "Level " + value.ToString(); } }

    public Button _extraTapBttn;
    public Text _selectLimitTxt;
    public int SelectLimitTxt { set { _selectLimitTxt.text = value.ToString(); } }

    public Animation _headerAnimation;
    public Animation _footerAnimation;

    void Start()
    {
        Activate();
        foreach (Ball.BallType ballType in LevelManager.Instance._ballTypeSet)
        {
            _colorBttnDict[ballType].gameObject.SetActive(true);
            _colorBttnList.Add(_colorBttnDict[ballType]);
        }
        SelectLimitTxt = LevelManager.Instance._selectLimit;
        LevelNumTxt = DataManager.Instance.LevelNum;
    }

    public void OnColorSelected(ColorBttn button)
    {
        if (_UIActivated)
        {
            _selectedColor = button._type;
            _frameTrs.gameObject.SetActive(true);
            _frameTrs.SetParent(button.transform);
            _frameTrs.localPosition = Vector2.zero;
            LevelManager.Instance._trainingHandler.ShowNext();
        }
    }

    internal void Activate()
    {
        _active = true;
        foreach (Button button in _colorBttnList)
            button.interactable = true;
    }

    internal void Deactivate()
    {
        _active = false;
        foreach (Button button in _colorBttnList)
            button.interactable = false;
    }

    internal void Show()
    {
        _headerAnimation["HeaderUp"].speed = -1f;
        _headerAnimation.Play();
        _footerAnimation["FooterDown"].speed = -1f;
        _footerAnimation.Play();
    }

    internal void Hide()
    {
        _headerAnimation.Play();
        _footerAnimation.Play();
    }

    public void OnRetry()
    {
        BallManager.Instance.GameOver(null);
    }

    public void OnExtraTap()
    {
        LevelManager.Instance.SelectLimit++;
        _extraTapBttn.interactable = false;
    }

}
