using UnityEngine;

public class SettingPanel : MonoBehaviour
{

    public GameObject _panel;

    public void Show()
    {
        _panel.SetActive(true);
    }

    public void Hide()
    {
        _panel.SetActive(false);
    }

    public void OnGotoColony11()
    {
        Application.OpenURL("http://www.colony11.com");
    }

    public void OnGotoPrivacyPlocy()
    {
        Application.OpenURL("https://www.privacypolicygenerator.info/live.php?token=NIRpdZlLoDiyvwZqkZQ8xUCCYk6Cpzdk");
    }

    public void OnGotoTermsAndConditions()
    {
        Application.OpenURL("https://www.termsandconditionsgenerator.com/live.php?token=MLJ4joX3hUzi32o0lP3B5qC1bZ6FnzfJ");
    }

    public void OnUnlockAd()
    {

    }

    public void OnClose()
    {
        Hide();
    }

}
