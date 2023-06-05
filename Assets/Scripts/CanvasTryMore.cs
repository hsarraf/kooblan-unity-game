using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasTryMore : MonoBehaviour
{
    private static CanvasTryMore _instance;
    public static CanvasTryMore Instance { get { return _instance; } }
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
            yield return new WaitForSeconds(0.1f);
            anim.Play();
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

    public void OnRetry()
    {
        GameManager.Instance.RetryLevel();
    }

    public void OnHome()
    {
        GameManager.Instance.GotoHome();
    }
}
