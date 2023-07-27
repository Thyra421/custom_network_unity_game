using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GUIManager : MonoBehaviour
{
    private void Update() {
        PointerEventData eventData = new PointerEventData(EventSystem.current) {
            position = Input.mousePosition
        };

        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raycastResults);

        ITooltipHandlerGUI tooltipHandler = null;
        IInteractableGUI interactable = null;

        for (int i = 0; i < raycastResults.Count; i++) {
            RaycastResult raycastResult = raycastResults[i];

            if (raycastResult.gameObject.TryGetComponent(out ITooltipHandlerGUI tooltipResult))
                tooltipHandler = tooltipResult;
            if (raycastResult.gameObject.TryGetComponent(out IInteractableGUI interactableResult))
                interactable = interactableResult;
        }

        TooltipGUIManager.Current.Tooltip = tooltipHandler;
        if (Input.GetMouseButtonDown(1))
            interactable?.Interact();
    }
}