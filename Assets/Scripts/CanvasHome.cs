using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CanvasHome : MonoBehaviour
{
    private static CanvasHome _instance;
    public static CanvasHome Instance { get { return _instance; } }
    void Awake()
    {
        if (_instance == null)
            _instance = this;
    }

    public Animation[] _CaptionanimationList;

    public Text _levelNumTxt;
    public int LevelNumTxt { set { _levelNumTxt.text = "Level " + value.ToString(); } }

    IEnumerator Start()
    {
        LevelNumTxt = DataManager.Instance.LevelNum;
        foreach(Animation anim in _CaptionanimationList)
        {
            yield return new WaitForSeconds(0.1f);
            anim.Play();
        }
    }

    public void OnPlay()
    {
        StartCoroutine(PlayCo());
    }

    IEnumerator PlayCo()
    {
        foreach (Animation anim in _CaptionanimationList)
        {
            foreach(AnimationState state in anim)
            {
                state.time = 0.3f;
                state.speed = -2f;
                anim.Play();
                yield return new WaitForSeconds(0.05f);
            }
        }
        yield return new WaitForSeconds(0.1f);
        GameManager.Instance.Play();
    }

    public SettingPanel _settingPanel;
    public void OnSetting()
    {
        _settingPanel.Show();
    }

}
