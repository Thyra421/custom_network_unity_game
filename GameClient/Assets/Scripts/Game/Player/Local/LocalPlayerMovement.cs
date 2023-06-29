using TMPro;
using UnityEngine;

public class LocalPlayerMovement : PlayerMovement
{
    [Header("Components")]
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
    [SerializeField]
    private LayerMask _walkableLayer;

    private Vector3 _direction;
    private float _verticalVelocity;
    private float _currentSpeed;
    private Vector3 _hitNormal;
    private Vector3 _hitPoint;

    private void MoveInDirection() {
        Vector3 movingDirection = _direction * _currentSpeed;
        movingDirection = transform.rotation * movingDirection;
        Vector3 verticalDirection = Vector3.up * _verticalVelocity;

        if (IsOnSlope) {
            Vector3 perpendicular = Vector3.Cross(_hitNormal, Vector3.up);
            movingDirection = perpendicular.normalized * movingDirection.magnitude * (Vector3.Dot(perpendicular, movingDirection) > 0 ? 1 : -1) / 2;
            Quaternion slopeRotation = Quaternion.AngleAxis(90, perpendicular);
            Vector3 slopeDirection = slopeRotation * _hitNormal;
            Debug.DrawRay(transform.position, slopeDirection);
            verticalDirection += slopeDirection * _verticalVelocity;
        }

        _characterController.Move((movingDirection + verticalDirection) * Time.deltaTime);
    }

    void OnControllerColliderHit(ControllerColliderHit hit) {
        _hitNormal = hit.normal;
        _hitPoint = hit.point;
    }

    protected override void Move() {
        _verticalVelocity = Mathf.Clamp(_verticalVelocity + _gravityStrength * Time.deltaTime, _gravityStrength, Mathf.Infinity);
        MoveInDirection();
    }

    private void Update() {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 input = new Vector3(horizontalInput, 0, verticalInput);
        input.Normalize();

        if (IsGrounded && !IsOnSlope) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                _verticalVelocity = Mathf.Sqrt(_jumpHeight * -2f * _gravityStrength);
                _animator.SetTrigger("Jump");
            }
        }

        if (input.magnitude > 0) {
            _direction = input;
            _currentSpeed = Mathf.Clamp(_currentSpeed + _acceleration * Time.deltaTime, 0, _movementSpeed);
        } else {
            _direction = Vector3.Lerp(_direction, Vector3.zero, Time.deltaTime * _deceleration);
            _currentSpeed = Mathf.Clamp(_currentSpeed - _deceleration * Time.deltaTime, 0, _movementSpeed);
            if (_currentSpeed < 1)
                _direction = Vector3.zero;
        }

        _animator.SetBool("IsGrounded", IsGrounded);
        _animator.SetBool("IsRunning", input.magnitude > 0);
        _animator.SetFloat("X", _direction.x);
        _animator.SetFloat("Y", _direction.z);
    }

    protected override void Rotate() {
        //handled by camera
    }

    public Vector3 Movement {
        get => _direction;
        set => _direction = value;
    }

    private void OnDrawGizmos() {
        Gizmos.DrawSphere(transform.position, .2f);
        Gizmos.DrawSphere(_hitPoint, .1f);
    }

    public bool IsOnSlope => Vector3.Angle(Vector3.up, _hitNormal) >= _characterController.slopeLimit;

    public bool IsGrounded => Physics.CheckSphere(transform.position, .2f, _walkableLayer);

    public AnimationData Animation => new AnimationData(_animator.GetFloat("X"), _animator.GetFloat("Y"), _animator.GetBool("IsRunning"), _animator.GetBool("IsGrounded"));
}