using UnityEngine;
using UnityEngine.UI;

public class DragAndDropGUIManager : MonoBehaviour
{
    [SerializeField]
    private Image _dragIndicator;
    private IDraggableGUI _tempDraggable;
    private IDraggableGUI _currentDraggable;
    private Vector3 _dragOrigin;

    public static DragAndDropGUIManager Current { get; private set; }
    public bool IsDragging => _currentDraggable != null;
    public bool IsPreparedToDrag => _tempDraggable != null;

    private void BeginDrag() {
        _currentDraggable = _tempDraggable;
        _dragIndicator.sprite = _currentDraggable.DragIndicator;
        _dragIndicator.gameObject.SetActive(true);
    }

    private void EndDrag() {
        _tempDraggable = null;
        _currentDraggable = null;
        _dragIndicator.gameObject.SetActive(false);
    }

    private void Awake() {
        if (Current == null)
            Current = this;
        else
            Destroy(gameObject);
    }

    private void LateUpdate() {
        if (!IsDragging && IsPreparedToDrag && Vector3.Distance(Input.mousePosition, _dragOrigin) > 10)
            BeginDrag();

        if (IsDragging)
            _dragIndicator.rectTransform.position = Input.mousePosition;
    }

    public void Discard() {
        _currentDraggable.Discard();
        EndDrag();
    }

    public void DropIn(IDropAreaGUI dropArea) {
        dropArea.OnDrop(_currentDraggable);
        EndDrag();
    }

    public void CancelDrag() {
        _tempDraggable = null;
    }

    public void PrepareDrag(IDraggableGUI draggable, Vector3 position) {
        _tempDraggable = draggable;
        _dragOrigin = position;
    }    
}