using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(CapsuleCollider))]
public class LocalPlayerMovement : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private LocalPlayer _localPlayer;
    [SerializeField]
    private CharacterController _characterController;

    [Header("Movement")]
    [SerializeField]
    private float _acceleration = 30f;
    [SerializeField]
    private float _deceleration = 40f;
    [SerializeField]
    private float _jumpHeight = 1f;

    [Header("Physics")]
    [SerializeField]
    private float _gravityStrength = -10;

    private Vector3 _direction;
    private Vector3 _hitNormal;
    private Vector3 _input;
    private float _verticalVelocity;
    private float _currentSpeed;
    private bool _isOnSlope;
    private bool _isGrounded;
    private bool _isRunning;

    private bool IsGrounded {
        get => _isGrounded;
        set {
            if (value != _isGrounded) {
                _isGrounded = value;
                _localPlayer.Animation.SetBool("IsGrounded", _isGrounded);
            }
        }
    }
    private bool IsRunning {
        set {
            if (value != _isRunning) {
                _isRunning = value;
                _localPlayer.Animation.SetBool("IsRunning", _isRunning);
            }
        }
    }

    private float MovementSpeed => StatisticsManager.Current.Find(StatisticType.MovementSpeed).Value * SharedConfig.Current.PlayerMovementSpeed;

    public bool CanMove => !(StatesManager.Current.Find(StateType.Rooted).Value || StatesManager.Current.Find(StateType.Stunned).Value);

    private void Move() {
        Vector3 movingDirection = _direction * _currentSpeed;
        movingDirection = transform.rotation * movingDirection;
        Vector3 verticalDirection = Vector3.up * _verticalVelocity;

        if (_isOnSlope) {
            Vector3 perpendicular = Vector3.Cross(_hitNormal, Vector3.up);
            movingDirection = (Vector3.Dot(perpendicular, movingDirection) > 0 ? 1 : -1) * movingDirection.magnitude * perpendicular.normalized / 2;
            Quaternion slopeRotation = Quaternion.AngleAxis(90, perpendicular);
            Vector3 slopeDirection = slopeRotation * _hitNormal;
            verticalDirection += slopeDirection * _verticalVelocity;
        }

        _characterController.Move((movingDirection + verticalDirection) * Time.deltaTime);
    }

    private void HandleGravity() {
        _verticalVelocity = Mathf.Clamp(_verticalVelocity + _gravityStrength * Time.deltaTime, _gravityStrength, Mathf.Infinity);
    }

    private void HandleAnimations() {
        _localPlayer.Animation.SetFloat("X", _direction.x);
        _localPlayer.Animation.SetFloat("Y", _direction.z);
    }

    private void HandleJump() {
        if (CanMove && IsGrounded && !_isOnSlope && Input.GetKeyDown(KeyCode.Space)) {
            _verticalVelocity = Mathf.Sqrt(_jumpHeight * -2f * _gravityStrength);
            _localPlayer.Animation.SetTrigger("Jump");
        }
    }

    private void HandleMovement() {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        _input = new Vector3(horizontalInput, 0, verticalInput);
        _input.Normalize();

        if (_input.magnitude > 0 && CanMove) {
            _direction = _input;
            _currentSpeed = Mathf.Clamp(_currentSpeed + _acceleration * Time.deltaTime, 0, MovementSpeed);
        } else {
            _currentSpeed = Mathf.Clamp(_currentSpeed - _deceleration * Time.deltaTime, 0, MovementSpeed);
            if (_currentSpeed < 1)
                _direction = Vector3.zero;
            else
                _direction = Vector3.Lerp(_direction, Vector3.zero, Time.deltaTime * _deceleration);
        }
    }

    private void Update() {
        HandleJump();
        HandleMovement();
        HandleGravity();
        HandleAnimations();

        IsRunning = _direction.magnitude > 0;
        IsGrounded = Physics.CheckSphere(transform.position, .1f, Config.Current.WhisIsGround);
    }

    private void FixedUpdate() {
        Move();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit) {
        _hitNormal = hit.normal;
        _isOnSlope = Vector3.Angle(Vector3.up, _hitNormal) >= _characterController.slopeLimit;
    }
}