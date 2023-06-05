using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

using GameAnalyticsSDK;


public class GameManager : MonoBehaviour
{
    private static GameManager __instance;
    public static GameManager Instance { get { return __instance; } }
    void Awake()
    {
        if (__instance == null)
        {
            __instance = this;
            DontDestroyOnLoad(gameObject);
            GameAnalytics.Initialize();
        }
        else if (__instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void GameOver(bool? win)
    {
        if (win == true)
        {
            DataManager.Instance.LevelNum++;
            /* show loading page */
        }
        else
        {
            /* show pause panel */
        }
    }

    public void Play()
    {
        CanvasLoading.Instance.Show();
        StartCoroutine(PlayCo());
    }
    IEnumerator PlayCo()
    {
        yield return null;
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("ingame");
        asyncOperation.allowSceneActivation = false;
        while (!asyncOperation.isDone)
        {
            if (asyncOperation.progress >= 0.9f)
                asyncOperation.allowSceneActivation = true;
            yield return null;
        }
        //yield return new WaitForSeconds(1f);
        CanvasLoading.Instance.Hide();
    }

    internal void GotoNextLevel()
    {
        Play();
    }

    internal void RetryLevel()
    {
        Play();
    }

    internal void GotoHome()
    {
        SceneManager.LoadSceneAsync("main");
    }

}