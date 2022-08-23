using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ball : MonoBehaviour 
{
    [SerializeField] private InputAction _mouseRightClick;
    [SerializeField] private Transform _forceDirection;
    [SerializeField] private float _force;
    private Rigidbody _rb;
    private Camera _cam;
    private Line _line;
    private float _lineLenght;
    private bool _isShot;
    private Func<bool> _testFunc;
    private bool _isAiming;
    private bool _canRotate;
    private int _totalHits;

    private void Awake() 
    {
        _rb = GetComponent<Rigidbody>();
        _cam = Camera.main;
        _line = FindObjectOfType<Line>();
    }

    private void Start() {
        _testFunc = () => _canRotate;
    }

    private void FixedUpdate() 
    {
        if (!_isAiming && !_isShot && _lineLenght != 0) {
            _rb.AddForce(_forceDirection.forward * _force * _lineLenght, ForceMode.Impulse);
            _isShot = true;
            _totalHits++;
        }
    }

    private void Update() {
        CheckForRotation();
        if (!_isAiming) { return; }
        _lineLenght = _line.GetLineLenght() * 10;
        _isShot = false;
    }

    private void OnEnable() {
        _mouseRightClick.Enable();
        _mouseRightClick.performed += MousePressed;
    }

    private void OnDisable() {
        _mouseRightClick.Disable();
        _mouseRightClick.performed -= MousePressed;
    }

    private void MousePressed(InputAction.CallbackContext contex)
    {
        Vector3 initialPos = _cam.ScreenToViewportPoint(Mouse.current.position.ReadValue());
    
        StartCoroutine(RotateCamera(initialPos));
    }

    private IEnumerator RotateCamera(Vector3 initialPos)
    {   
        while (_mouseRightClick.ReadValue<float>() != 0 ) {
            if (_canRotate) { 
                Vector3 direction = initialPos - _cam.ScreenToViewportPoint(Mouse.current.position.ReadValue());

                _cam.transform.RotateAround(transform.position, Vector3.down, direction.x * 180f);
                _cam.transform.RotateAround(transform.position, Vector3.right, direction.y * 180f);

                initialPos = _cam.ScreenToViewportPoint(Mouse.current.position.ReadValue());
                yield return new WaitForFixedUpdate();
            } else {
                yield return new WaitUntil(_testFunc);
            }
        }
    }

    private void CheckForRotation() {
        if (_rb.velocity == Vector3.zero) {
            _canRotate = true;
        } else {
            _canRotate = false;
        }
    }

    public bool GetAiming() {
        return _isAiming;
    }

    public void SetAiming(bool state) {
        _isAiming = state;
    }

    public bool GetRotationOption() {
        return _canRotate;
    }

    public void SetRotationOption(bool state) {
        _canRotate = state;
    }

    public int GetTotalHits() {
        return _totalHits;
    }
}
