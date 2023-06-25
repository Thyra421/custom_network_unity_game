using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform _target;
    [SerializeField]
    private Transform _player;
    [SerializeField]
    private float _zoomSpeed = 5f;
    [SerializeField]
    private float _rotationSpeed = 3f;
    [SerializeField]
    private float _initialDistance = 5f;
    [SerializeField]
    private LayerMask _collisionLayers;
    [SerializeField]
    private float _minDistance = 0;
    [SerializeField]
    private float _maxDistance = 7;
    private Vector3 _offset;
    private float _currentDistance;
    private float _xRotation = 0f;
    private float _yRotation = 0f;

    private void Start() {
        _offset = transform.position - _target.position;
        _currentDistance = _initialDistance;
    }

    private void LateUpdate() {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        _currentDistance -= scroll * _zoomSpeed;
        _currentDistance = Mathf.Clamp(_currentDistance, _minDistance, _maxDistance);

        if (Input.GetMouseButton(0)) {
            _xRotation += Input.GetAxis("Mouse X") * _rotationSpeed;
            _yRotation -= Input.GetAxis("Mouse Y") * _rotationSpeed;
            _yRotation = Mathf.Clamp(_yRotation, -60f, 60f);
        }
        if (Input.GetMouseButton(1)) {
            _xRotation += Input.GetAxis("Mouse X") * _rotationSpeed;
            _yRotation -= Input.GetAxis("Mouse Y") * _rotationSpeed;
            _yRotation = Mathf.Clamp(_yRotation, -60f, 60f);
            _player.rotation = Quaternion.Euler(0f, _xRotation, 0f);
        }

        Quaternion rotation = Quaternion.Euler(_yRotation, _xRotation, 0f);
        Vector3 desiredPosition = _target.position + rotation * _offset * _currentDistance;


        if (Physics.Linecast(_target.position, desiredPosition, out RaycastHit hit, _collisionLayers))
            desiredPosition = hit.point;

        transform.SetPositionAndRotation(desiredPosition, rotation);
    }
}