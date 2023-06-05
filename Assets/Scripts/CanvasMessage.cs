using UnityEngine;
using UnityEngine.UI;

public class CanvasMessage : MonoBehaviour
{
    private static CanvasMessage _instance;
    public static CanvasMessage Instance { get { return _instance; } }
    void Awake()
    {
        if (_instance == null)
            _instance = this;
    }

    public Animation _animation;
    public Text _msgTxt;
    internal void ShowMessage(string msg)
    {
        _msgTxt.text = msg;
        _animation.Play();
    }
}
