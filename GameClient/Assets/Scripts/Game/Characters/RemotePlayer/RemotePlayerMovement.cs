using UnityEngine;

public class RemotePlayerMovement : Movement
{
    [SerializeField]
    protected float _rotationSpeed = 100f;
    private Vector3 _destinationPosition;
    private Vector3 _destinationRotation;
    private PlayerAnimationData _playerAnimationData;

    protected override void Move() {
        Vector3 direction = (_destinationPosition - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, _destinationPosition);

        _animator.SetBool("IsRunning", _playerAnimationData.isRunning);
        _animator.SetBool("IsGrounded", _playerAnimationData.isGrounded);
        _animator.SetFloat("X", _playerAnimationData.x);
        _animator.SetFloat("Y", _playerAnimationData.y);
        if (distance <= _movementSpeed * Time.deltaTime || distance > 2) {
            transform.position = _destinationPosition;
        } else {
            transform.position += _movementSpeed * Time.deltaTime * direction;
        }
    }

    protected override void Rotate() {
        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, _destinationRotation, Time.deltaTime * _rotationSpeed);
    }

    private void Awake() {
        _destinationPosition = transform.position;
        _destinationRotation = transform.eulerAngles;
    }

    public Vector3 DestinationPosition {
        get => _destinationPosition;
        set => _destinationPosition = value;
    }
    public Vector3 DestinationRotation {
        get => _destinationRotation;
        set => _destinationRotation = value;
    }
    public PlayerAnimationData PlayerAnimationData {
        get => _playerAnimationData;
        set => _playerAnimationData = value;
    }
}