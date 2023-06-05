using System.Collections;
using UnityEngine;

public class CanvasWin : MonoBehaviour
{
    private static CanvasWin _instance;
    public static CanvasWin Instance { get { return _instance; } }
    void Awake()
    {
        if (_instance == null)
            _instance = this;
    }

    public GameObject _panel;
    public Animation[] _animationList;

    public void Show()
    {
        _panel.SetActive(true);
        StartCoroutine(ShowCo());
    }
    IEnumerator ShowCo()
    {
        foreach (Animation anim in _animationList)
        {
            anim.Play();
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void Hide()
    {
        StartCoroutine(HideCo());
    }

    IEnumerator HideCo()
    {
        foreach (Animation anim in _animationList)
        {
            foreach (AnimationState state in anim)
            {
                state.time = 0.3f;
                state.speed = -1f;
                anim.Play();
                yield return new WaitForSeconds(0.1f);
            }
        }
        yield return new WaitForSeconds(0.1f);
        _panel.SetActive(false);
    }

    public void OnNext()
    {
        GameManager.Instance.GotoNextLevel();
    }

}
