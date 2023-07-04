using UnityEngine;
using UnityEngine.UI;

public class DragAndDropGUI : MonoBehaviour
{
    private static DragAndDropGUI _current;
    [SerializeField]
    private Image _dragIndicator;

    private void Awake() {
        if (_current == null)
            _current = this;
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

    public static DragAndDropGUI Current => _current;
}