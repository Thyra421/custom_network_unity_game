using UnityEngine;

public class CombatGUIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _aimTarget;
    [SerializeField]
    private CameraController _cameraController;

    private void OnStartZoomInAim() {
        _aimTarget.SetActive(true);
    }

    private void OnEndZoomInAim() {
        _aimTarget.SetActive(false);
    }

    private void Awake() {
        _cameraController.OnStartZoomInAim += OnStartZoomInAim;
        _cameraController.OnEndZoomInAim += OnEndZoomInAim;
    }
}