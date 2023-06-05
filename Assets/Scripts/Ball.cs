using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public List<Ball> _nextList;

    public enum BallType
    {
        none, solid, red, green, blue, cyan, magenta, yellow
    }
    public BallType _type;
    public BallType Type
    {
        set
        {
            _type = value;
            _renderer.sprite = BallManager.Instance._ballDict[value];
        }
    }

    internal Transform _transform;
    internal SpriteRenderer _renderer;

    Vector2 _initPos;
    internal Vector2 _initScale;
    public bool _dirty = false;
    public bool _trigger = false;

    public bool _finished = false;

    public ParticleSystem _fxPart1;

    public int _i;
    public int _j;

    void Awake()
    {
        _nextList = new List<Ball>();
        _transform = GetComponent<Transform>();
        _renderer = GetComponent<SpriteRenderer>();
        float s = Mathf.Lerp(0.06f, 0.07f, Mathf.InverseLerp(0.5f, 0.75f, CameraHandler.Instance._camera.aspect)) * 0.8f;
        _transform.localScale = new Vector2(s, s);
        _initScale = _transform.localScale;
        _initPos = _transform.position;
    }

    //float _timer = 0f;
    //void Update()
    //{
    //    if (_trigger)
    //    {
    //        _transform.localScale = Vector2.Lerp(_transform.localScale, _initScale * 3f, Time.deltaTime * 4f);
    //        _timer += Time.deltaTime;
    //        if (_timer > 0.3f * Time.timeScale)
    //        {
    //            _trigger = false;
    //            _timer = 0f;
    //        }
    //    }
    //    else
    //        _transform.localScale = Vector2.Lerp(_transform.localScale, _initScale, Time.deltaTime * 4f);
    //}

    public void SetGrey(float gs)
    {
        _renderer.color = new Color(gs, gs, gs);
        //_renderer.color = Color.white * _grey;
    }

    public void SetGrey(Color c)
    {
        _renderer.color = c;
    }

    public void SetIndex(int i, int j)
    {
        _i = i;
        _j = j;
    }

    void Finish()
    {
        _finished = true;
    }

}
