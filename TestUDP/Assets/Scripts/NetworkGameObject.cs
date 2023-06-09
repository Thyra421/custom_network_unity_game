using System.Linq;
using UnityEngine;

public class NetworkGameObject : MonoBehaviour
{
    public string id;

    public Vector3 destination;      // Destination transform to move towards
    public float movementSpeed = 5f;   // Speed of the movement

    public static NetworkGameObject Find(string id) {
        NetworkGameObject[] networkGameObjects = FindObjectsOfType<NetworkGameObject>();
        NetworkGameObject networkGameObject = networkGameObjects.First((NetworkGameObject g) => g.id == id);
        return networkGameObject;
    }

    public void SetDestination(Vector3 position) {
        destination = position;
    }

    private void Update() {
        // Calculate the direction towards the destination
        Vector3 direction = (destination - transform.position).normalized;

        // Calculate the distance to the destination
        float distance = Vector3.Distance(transform.position, destination);

        // Check if the object has reached the destination
        if (distance <= movementSpeed * Time.deltaTime) {
            // Snap to the destination position
            transform.position = destination;
        } else {
            // Move towards the destination with a constant speed
            transform.position += direction * movementSpeed * Time.deltaTime;
        }
    }
}
