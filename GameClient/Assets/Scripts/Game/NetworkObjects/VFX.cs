using UnityEngine;

public class VFX : NetworkObject
{
    private float _movementSpeed = 10;
    public Vector3 DestinationPosition { get; set; }

    private void FixedUpdate() {
        Vector3 direction = (DestinationPosition - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, DestinationPosition);

        if (distance <= _movementSpeed * Time.deltaTime) {
            transform.position = DestinationPosition;
        } else {
            transform.position += _movementSpeed * Time.deltaTime * direction;
        }
    }
}