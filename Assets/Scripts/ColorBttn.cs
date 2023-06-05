using UnityEngine;
using UnityEngine.UI;

public class ColorBttn : MonoBehaviour
{
    Button _button;
    public Ball.BallType _type;

    void Awake()
    {
        _button = GetComponent<Button>();
    }

}
