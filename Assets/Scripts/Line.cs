using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Line : MonoBehaviour 
{
    [SerializeField] private InputAction _mouseLeftClick;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private Transform _ballTransform;
    
    private Camera _mainCamera;
    private Ball _ball;
    private float _lineLenght;

    private void Awake() {
        _mainCamera = Camera.main;
        _lineRenderer.gameObject.SetActive(false);
        _ball = FindObjectOfType<Ball>();
    }
    
    private void OnEnable() {
        _mouseLeftClick.Enable();
        _mouseLeftClick.performed += MousePressed;
    }

    private void OnDisable() {
        _mouseLeftClick.Disable();
        _mouseLeftClick.performed -= MousePressed;
    }

    private void MousePressed(InputAction.CallbackContext contex) {
        Vector3 mousePosInitial = _mainCamera.ScreenToViewportPoint(Mouse.current.position.ReadValue());
        StartCoroutine(Draw(mousePosInitial));
    }

    private IEnumerator Draw(Vector3 initialPosition) {
        while (_mouseLeftClick.ReadValue<float>() != 0) {
            _ball.SetAiming(true);
            Vector3 mousePos = _mainCamera.ScreenToViewportPoint(Mouse.current.position.ReadValue());
            Vector3 heading = mousePos - _ballTransform.position;
            _lineLenght = Mathf.Clamp((Mathf.Abs(mousePos.y - initialPosition.y)) / 2, 0f, .2f);
            Vector3 linePos = new Vector3(_ballTransform.position.x, _ballTransform.position.y + (_ballTransform.localScale.y / 20), _ballTransform.position.z);
            Vector3 lineEnd = new Vector3(_ballTransform.position.x, linePos.y + _lineLenght, _ballTransform.position.z);

            _lineRenderer.gameObject.SetActive(true);
            _lineRenderer.SetPosition(0, linePos);
            _lineRenderer.SetPosition(1, lineEnd);
            yield return new WaitForFixedUpdate();
        }
        _ball.SetAiming(false);
        _lineRenderer.gameObject.SetActive(false);
        _lineLenght = 0f;
    }

    public float GetLineLenght() {
        return _lineLenght;
    }
}
