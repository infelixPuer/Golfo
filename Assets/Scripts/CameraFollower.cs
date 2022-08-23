using UnityEngine;

public class CameraFollower : MonoBehaviour 
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _smoothSpeed;
    private Ball _ball;
    private float _distance;
    private bool _writeOnce;
    private Vector3 _heading;
    private Vector3 _direction;

    private void Awake() {
        _ball = FindObjectOfType<Ball>();
    }

    private void Start() 
    {
        _heading = _target.position - transform.position;
        
    }

    private void Update() 
    {
        transform.LookAt(_target.position, Vector3.up);

        if (_ball.GetRotationOption()) { return; }

        FollowOnDistance();
    }

    private void FollowOnDistance() {
        if (_ball.GetAiming()) {
            _heading = _target.position - transform.position;
        }

        Vector3 desiredPosition = _target.position - _heading;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }
}
