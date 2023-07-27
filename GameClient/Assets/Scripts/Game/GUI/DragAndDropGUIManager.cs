using UnityEngine;
using UnityEngine.UI;

public class DragAndDropGUIManager : MonoBehaviour
{
    [SerializeField]
    private Image _dragIndicator;

    public static DragAndDropGUIManager Current { get; private set; }

    private void Awake() {
        if (Current == null)
            Current = this;
        else
            Destroy(gameObject);
    }

    private void Update() {
        if (_dragIndicator.sprite != null)
            _dragIndicator.rectTransform.position = Input.mousePosition;
    }

    public void StartDrag(Sprite sprite) {
        _dragIndicator.gameObject.SetActive(true);
        _dragIndicator.sprite = sprite;
    }

    public void StopDrag() {
        _dragIndicator.gameObject.SetActive(false);
        _dragIndicator.sprite = null;
    }
}