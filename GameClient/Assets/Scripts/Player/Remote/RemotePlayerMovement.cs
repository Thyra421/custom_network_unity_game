using UnityEngine;

public class RemotePlayerMovement : PlayerMovement
{
    private Vector3 _destinationPosition;
    private Vector3 _destinationRotation;
    private AnimationData _animationData;

    protected override void Move() {
        Vector3 direction = (_destinationPosition - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, _destinationPosition);

        if (distance == 0) {
            _animator.SetBool("IsRunning", false);
        } else if (distance <= _movementSpeed * Time.deltaTime) {
            transform.position = _destinationPosition;
        } else {
            _animator.SetBool("IsRunning", true);
            _animator.SetFloat("X", _animationData.x);
            _animator.SetFloat("Y", _animationData.y);
            transform.position += direction * _movementSpeed * Time.deltaTime;
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
    public AnimationData AnimationData {
        get => _animationData;
        set => _animationData = value;
    }
}