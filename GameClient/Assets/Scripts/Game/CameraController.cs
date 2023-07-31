using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Target")]
    [SerializeField]
    private Transform _target;
    [SerializeField]
    private Transform _player;
    [Header("Movement")]
    [SerializeField]
    private float _zoomSpeed = 5f;
    [SerializeField]
    private float _zoomIncrement = 5f;
    [SerializeField]
    private float _rotationSpeed = 3f;
    [Header("Distance")]
    [SerializeField]
    private float _initialDistance = 5f;
    [SerializeField]
    private float _minDistance = 0;
    [SerializeField]
    private float _maxDistance = 7;
    [SerializeField]
    private float _aimDistance = .5f;
    [SerializeField]
    private float _aimSpeed = 10f;
    [SerializeField]
    private float _aimZoomDistance = 2f;
    [Header("Physics")]
    [SerializeField]
    private LayerMask _collisionLayers;
    private Vector3 _offset;
    private float _currentDistance;
    private float _xRotation = 0f;
    private float _yRotation = 0f;
    private float _desiredDistance;
    private Vector3 _internalOffset;

    public static CameraController Current { get; private set; }
    public bool IsAiming { get; private set; }
    public bool IsBusy { get; private set; }
    public Vector3Data AimDirection => new Vector3Data(transform.forward);

    public delegate void OnStartZoomInAimHandler();
    public delegate void OnEndZoomInAimHandler();
    public event OnStartZoomInAimHandler OnStartZoomInAim;
    public event OnEndZoomInAimHandler OnEndZoomInAim;

    private void Awake() {
        if (Current == null)
            Current = this;
        else
            Destroy(gameObject);
    }

    private void Start() {
        _offset = transform.position - _target.position;
        _currentDistance = _initialDistance;
        _desiredDistance = _initialDistance;
    }

    private void LateUpdate() {
        if (IsBusy || !GUIManager.Current.IsBusy) {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
                IsBusy = true;
            if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
                IsBusy = false;

            float scroll = Input.GetAxis("Mouse ScrollWheel");
            _desiredDistance -= scroll * _zoomIncrement;
            _desiredDistance = Mathf.Clamp(_desiredDistance, _minDistance, _maxDistance);

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
        }

        if (IsAiming) {
            _currentDistance = Mathf.Lerp(_currentDistance, _aimZoomDistance, _aimSpeed * Time.deltaTime);
            _internalOffset = Vector3.Lerp(_internalOffset, transform.right * _aimDistance, _aimSpeed * Time.deltaTime);
        } else {
            _currentDistance = Mathf.Lerp(_currentDistance, _desiredDistance, _zoomSpeed * Time.deltaTime);
            _internalOffset = Vector3.Lerp(_internalOffset, Vector3.zero, _aimSpeed * Time.deltaTime);
        }

        Quaternion rotation = Quaternion.Euler(_yRotation, _xRotation, 0f);
        Vector3 desiredPosition = _target.position + rotation * _offset * _currentDistance + _internalOffset;

        if (Physics.Linecast(_target.position, desiredPosition, out RaycastHit hit, _collisionLayers))
            desiredPosition = hit.point + Vector3.up * .1f;

        transform.SetPositionAndRotation(desiredPosition, rotation);
    }

    public void StartAim() {
        IsAiming = true;
        OnStartZoomInAim?.Invoke();
    }

    public void StopAim() {
        IsAiming = false;
        OnEndZoomInAim?.Invoke();
    }
}