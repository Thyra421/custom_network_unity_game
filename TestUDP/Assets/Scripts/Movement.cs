using UnityEngine;

public class Movement : MonoBehaviour
{
    private float elapsedTime = 0f;
    private float thresholdTime = 0.05f;

    public float moveSpeed = 5f;

    private void Update() {
        // Get input axes for horizontal (left/right) and vertical (forward/backward) movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate the movement direction
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);

        // Normalize the movement vector to ensure constant speed regardless of direction
        movement.Normalize();

        // Apply the movement
        transform.Translate(moveSpeed * Time.deltaTime * movement);

        elapsedTime += Time.deltaTime;

        if (elapsedTime >= thresholdTime) {
            NetworkManager.current.udp.Send(new ClientMessagePosition(new Vector3Data(transform.position.x, transform.position.y, transform.position.z)));
            // Reset the timer
            elapsedTime = 0f;
        }
    }
}
