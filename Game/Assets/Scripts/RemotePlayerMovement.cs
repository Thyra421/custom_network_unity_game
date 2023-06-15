using UnityEngine;

public class RemotePlayerMovement : Movement
{
    private Vector3 _destination;

    public Vector3 Destination {
        get => _destination;
        set => _destination = value;
    }

    private void MoveTowardsDestination() {
        Vector3 direction = (_destination - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, _destination);

        if (distance == 0)
            _animator.SetBool("Run", false);
        else if (distance <= _movementSpeed * Time.deltaTime) {
            transform.position = _destination;
        } else {
            _animator.SetBool("Run", true);           
            transform.position += direction * _movementSpeed * Time.deltaTime;
        }
    }

    private void FixedUpdate() {
        MoveTowardsDestination();
    }
}