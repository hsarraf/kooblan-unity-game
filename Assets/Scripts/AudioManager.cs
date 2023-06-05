using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager __instance;
    public static AudioManager Instance { get { return __instance; } }
    void Awake()
    {
        if (__instance == null)
        {
            __instance = this;
            _audioCource = GetComponent<AudioSource>();
            DontDestroyOnLoad(gameObject);
        }
        else if (__instance != this)
        {
            Destroy(gameObject);
        }
    }

    AudioSource _audioCource;

    public AudioClip _poof;
    public AudioClip _colorTap;
    public AudioClip _click;
    public AudioClip _win;
    public AudioClip _lose;

    public void PlayPoof()
    {
        _audioCource.PlayOneShot(_poof);
    }

    public void PlayColorTap()
    {
        _audioCource.PlayOneShot(_colorTap);
    }

    public void PlayClick()
    {
        _audioCource.PlayOneShot(_click, 2f);
    }

    public void PlayWin()
    {
        _audioCource.PlayOneShot(_win);
    }

    public void PlayLose()
    {
        _audioCource.PlayOneShot(_lose);
    }

}
