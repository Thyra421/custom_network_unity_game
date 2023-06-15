using UnityEngine;

public class RemotePlayerMovement : Movement
{
    private Vector3 _destinationPosition;
    private Vector3 _destinationRotation;

    private void MoveTowardsDestination() {
        Vector3 direction = (_destinationPosition - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, _destinationPosition);

        if (distance == 0)
            _animator.SetBool("Run", false);
        else if (distance <= _movementSpeed * Time.deltaTime) {
            transform.position = _destinationPosition;
        } else {
            _animator.SetBool("Run", true);
            transform.position += direction * _movementSpeed * Time.deltaTime;
        }
    }

    private void RotateTowardsDestination() {
        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, _destinationRotation, Time.deltaTime * _rotationSpeed);
    }

    private void FixedUpdate() {
        MoveTowardsDestination();
        RotateTowardsDestination();
    }

    public Vector3 DestinationPosition {
        get => _destinationPosition;
        set => _destinationPosition = value;
    }
    public Vector3 DestinationRotation {
        get => _destinationRotation;
        set => _destinationRotation = value;
    }
}