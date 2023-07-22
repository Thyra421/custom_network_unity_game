using UnityEngine;

public class VFX : NetworkObject
{
    public float MovementSpeed { get; set; }
    public Vector3 DestinationPosition { get; set; }

    private void FixedUpdate() {
        Vector3 direction = (DestinationPosition - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, DestinationPosition);

        if (distance <= MovementSpeed * Time.deltaTime) {
            transform.position = DestinationPosition;
        } else {
            transform.position += MovementSpeed * Time.deltaTime * direction;
        }
    }
}