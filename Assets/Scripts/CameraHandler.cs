using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    private static CameraHandler _instance;
    public static CameraHandler Instance { get { return _instance; } }
    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            _transform = GetComponent<Transform>();
            _camera = GetComponent<Camera>();
        }
    }

    internal Transform _transform;
    internal Camera _camera;

//    void LateUpdate()
//    {
//        Ball ball = BallManager.Instance._triggeredBall;
//        if (ball  != null)
//        {
//            _transform.position = (new Vector3(ball._transform.position.x, ball._transform.position.y, _transform.position.z)) * 0.1f;
//            _transform.position = Vector3.Lerp(_transform.position, new Vector3(ball._transform.position.x, ball._transform.position.y, _transform.position.z), Time.deltaTime);
//        }
//    }

}
