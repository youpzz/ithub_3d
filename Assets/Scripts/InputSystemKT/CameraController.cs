using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float sensitivity = 0.1f;
    [SerializeField] private float zoomSpeed = 5f;
    [SerializeField] private float minDistance = 2f;
    [SerializeField] private float maxDistance = 10f;
    [SerializeField] private float smoothness = 10f;

    private PlayerControls _input;
    private Vector2 _lookInput;
    private float _zoomInput;
    private float _currentDistance;
    private float _pitch;
    private float _yaw;

    private void Awake()
    {
        _input = new PlayerControls();
        _currentDistance = (minDistance + maxDistance) / 2;
    }

    private void LateUpdate()
    {
        _lookInput = _input.Player.Look.ReadValue<Vector2>();
        _zoomInput = _input.Player.Zoom.ReadValue<float>();

        _yaw += _lookInput.x * sensitivity;
        _pitch -= _lookInput.y * sensitivity;
        _pitch = Mathf.Clamp(_pitch, -30f, 60f);

        _currentDistance = Mathf.Clamp(_currentDistance - _zoomInput * zoomSpeed, minDistance, maxDistance);

        Quaternion rotation = Quaternion.Euler(_pitch, _yaw, 0);
        Vector3 position = target.position - (rotation * Vector3.forward * _currentDistance);

        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * smoothness);
        transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime * smoothness);
    }

    private void OnEnable() => _input.Enable();
    private void OnDisable() => _input.Disable();

}