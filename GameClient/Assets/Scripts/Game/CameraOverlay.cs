using UnityEngine;

public class CameraOverlay : MonoBehaviour
{
    [SerializeField]
    private Transform _mainCamera;

    private void Update() {
        transform.SetPositionAndRotation(_mainCamera.position, _mainCamera.rotation);
    }
}
