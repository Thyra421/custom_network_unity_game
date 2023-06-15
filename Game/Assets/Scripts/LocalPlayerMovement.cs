using UnityEngine;

public class LocalPlayerMovement : Movement
{
    private void FixedUpdate() {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);
        movement.Normalize();

        if (movement.magnitude > 0) {
            _animator.SetBool("Run", true);
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * _rotationSpeed);

            transform.Translate(_movementSpeed * Time.deltaTime * Vector3.forward);
        } else
            _animator.SetBool("Run", false);
    }
}