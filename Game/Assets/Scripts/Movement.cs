using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 5f;

    private void FixedUpdate() {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);
        movement.Normalize();
        transform.Translate(moveSpeed * Time.deltaTime * movement);
    }
}
