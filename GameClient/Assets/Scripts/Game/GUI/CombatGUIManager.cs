using UnityEngine;

public class CombatGUIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _aimTarget;

    private void OnStartZoomInAim() {
        _aimTarget.SetActive(true);
    }

    private void OnEndZoomInAim() {
        _aimTarget.SetActive(false);
    }

    private void Awake() {
        CameraController.Current.OnStartZoomInAim += OnStartZoomInAim;
        CameraController.Current.OnEndZoomInAim += OnEndZoomInAim;
    }
}