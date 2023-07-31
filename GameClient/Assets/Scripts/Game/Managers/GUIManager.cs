using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GUIManager : MonoBehaviour
{
    [SerializeField]
    private DragAndDropGUIManager _dragAndDropGUIManager;
    [SerializeField]
    private TooltipGUIManager _tooltipGUIManager;
    private bool _isOverUI;

    public static GUIManager Current { get; private set; }
    public bool IsBusy => _isOverUI || _dragAndDropGUIManager.IsDragging;

    private void Awake() {
        if (Current == null)
            Current = this;
        else
            Destroy(gameObject);
    }

    private void Update() {
        if (CameraController.Current.IsBusy)
            return;

        Vector3 mousePosition = Input.mousePosition;
        PointerEventData eventData = new PointerEventData(EventSystem.current) {
            position = mousePosition
        };
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raycastResults);

        _isOverUI = raycastResults.Count > 0;
        if (!IsBusy) {
            _tooltipGUIManager.Tooltip = null;
            return;
        }

        ITooltipHandlerGUI tooltipHandler = null;
        IInteractableGUI interactable = null;
        IDraggableGUI draggable = null;
        IDropAreaGUI dropArea = null;

        foreach (RaycastResult raycastResult in raycastResults) {
            if (raycastResult.gameObject.TryGetComponent(out ITooltipHandlerGUI tooltipResult))
                tooltipHandler = tooltipResult;
            if (raycastResult.gameObject.TryGetComponent(out IInteractableGUI interactableResult))
                interactable = interactableResult;
            if (raycastResult.gameObject.TryGetComponent(out IDraggableGUI draggableResult))
                draggable = draggableResult;
            if (raycastResult.gameObject.TryGetComponent(out IDropAreaGUI dropAreaResult))
                dropArea = dropAreaResult;
        }

        if (Input.GetMouseButtonDown(0)) {
            if (draggable != null && draggable.IsReadyToBeDragged)
                _dragAndDropGUIManager.PrepareDrag(draggable, mousePosition);
        } else if (Input.GetMouseButtonUp(0)) {
            if (_dragAndDropGUIManager.IsDragging) {
                if (dropArea == null)
                    _dragAndDropGUIManager.Discard();
                else
                    _dragAndDropGUIManager.DropIn(dropArea);
            } else if (_dragAndDropGUIManager.IsPreparedToDrag)
                _dragAndDropGUIManager.CancelDrag();
        } else if (Input.GetMouseButtonDown(1) && !_dragAndDropGUIManager.IsDragging)
            interactable?.Interact();

        if (_dragAndDropGUIManager.IsDragging)
            _tooltipGUIManager.Tooltip = null;
        else
            _tooltipGUIManager.Tooltip = tooltipHandler;
    }
}
