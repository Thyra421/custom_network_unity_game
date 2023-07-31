using UnityEngine;

public class VFX : NetworkObject
{
    private float _movementSpeed;
    private Vector3 _destinationPosition;

    private void FixedUpdate() {
        Vector3 direction = (_destinationPosition - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, _destinationPosition);

        if (distance <= _movementSpeed * Time.deltaTime) {
            transform.position = _destinationPosition;
        } else {
            transform.position += _movementSpeed * Time.deltaTime * direction;
        }
    }

    public void SetMovement(Vector3 destinationPosition, float movementSpeed) {
        _destinationPosition = destinationPosition;
        _movementSpeed = movementSpeed;
    }
}