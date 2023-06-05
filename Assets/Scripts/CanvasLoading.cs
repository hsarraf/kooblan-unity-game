using System.Collections;
using UnityEngine;

public class CanvasLoading : MonoBehaviour
{
    private static CanvasLoading __instance;
    public static CanvasLoading Instance { get { return __instance; } }
    void Awake()
    {
        if (__instance == null)
        {
            __instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (__instance != this)
        {
            Destroy(gameObject);
        }
    }

    public GameObject _panel;

    public Animation[] _anims;

    public void Show()
    {
        _panel.SetActive(true);
        StartCoroutine(ShowCo());
    }

    public void Hide()
    {
        StartCoroutine(HideCo());
    }

    IEnumerator HideCo()
    {
        yield return new WaitForSeconds(1f);
        StopAllCoroutines();
        _panel.SetActive(false);
    }

    IEnumerator ShowCo()
    {
        int i = 0;
        while(true)
        {
            _anims[i].Play();
            i = (i + 1) % 4;
            yield return new WaitForSeconds(0.05f);
        }
    }

    //private void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //        Show();
    //    if (Input.GetMouseButtonDown(1))
    //        Hide();
    //}

}
