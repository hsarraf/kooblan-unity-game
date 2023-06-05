using System.Collections.Generic;
using UnityEngine;

public class TrainingHandler : MonoBehaviour
{

    public GameObject _hand;

    SpriteRenderer _spriteRenderer;

    public List<Vector2> _handPosList;

    void Awake()
    {
        _spriteRenderer = _hand.GetComponent<SpriteRenderer>();
        ShowNext();
    }

    public void ShowNext()
    {
        if (DataManager.Instance.LevelNum == 1 && _handPosList.Count > 0)
        {
            _hand.transform.position = _handPosList[0];
            _hand.SetActive(true);
            _handPosList.RemoveAt(0);
        }
    }

    public void Hide()
    {
        _hand.SetActive(false);
    }

    void LateUpdate()
    {
        float alpha = Mathf.Abs(Mathf.Sin(Time.time * 3f));
        _spriteRenderer.color = new Color(1f, 1f, 1f, alpha);
    }

}
