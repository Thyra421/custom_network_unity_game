using UnityEngine;

public class RemotePlayerMovement : CharacterMovement
{
    private Vector3 _destinationPosition;
    private Vector3 _destinationRotation;
    private float _movementSpeed;

    protected override void Move() {
        Vector3 direction = (_destinationPosition - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, _destinationPosition);

        if (distance <= _movementSpeed * Time.deltaTime || distance > 2)
            transform.position = _destinationPosition;
        else
            transform.position += _movementSpeed * Time.deltaTime * direction;
    }

    protected override void Rotate() {
        transform.eulerAngles = _destinationRotation;
    }

    private void Awake() {
        _destinationPosition = transform.position;
        _destinationRotation = transform.eulerAngles;
    }

    public void SetMovement(TransformData transformData, float movementSpeed) {
        _destinationPosition = transformData.position.ToVector3;
        _destinationRotation = transformData.rotation.ToVector3;
        _movementSpeed = movementSpeed;
    }
}