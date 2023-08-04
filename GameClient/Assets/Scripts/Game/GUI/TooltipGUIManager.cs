using UnityEngine;
using UnityEngine.UI;

public class TooltipGUIManager : MonoBehaviour
{
    [SerializeField]
    private RectTransform _parent;
    [SerializeField]
    private VerticalLayoutGroup _verticalLayoutGroup;
    private ITooltipHandlerGUI _currentTooltipHandler;

    public ITooltipHandlerGUI Tooltip {
        get => _currentTooltipHandler;
        set {
            if (value != _currentTooltipHandler) {
                _currentTooltipHandler = value;
                if (_currentTooltipHandler != null && _currentTooltipHandler.IsTooltipReady) {
                    _parent.gameObject.SetActive(true);
                    Rebuild();
                } else
                    _parent.gameObject.SetActive(false);
            }
        }
    }

    private void Clear() {
        for (int i = 0; i < _parent.childCount; i++)
            Destroy(_parent.GetChild(i).gameObject);
    }

    private void Build() {
        _currentTooltipHandler.BuildTooltip(_parent);
        LayoutRebuilder.ForceRebuildLayoutImmediate(_parent);
    }

    private void Rebuild() {
        Clear();
        Build();
    }

    private void LateUpdate() {
        if (_currentTooltipHandler == null)
            return;

        Vector2 pivot = Vector2.zero;
        Vector2 position = Vector2.zero;

        RectTransform handler = _currentTooltipHandler.RectTransform;

        if (handler.position.x >= Screen.width / 2) {
            pivot.x = 1;
            position.x = handler.position.x - handler.rect.size.x * handler.lossyScale.x / 2;
        } else {
            pivot.x = 0;
            position.x = handler.position.x + handler.rect.size.x * handler.lossyScale.x / 2;
        }
        if (handler.position.y >= Screen.height / 2) {
            pivot.y = 1;
            position.y = handler.position.y - handler.rect.size.y * handler.lossyScale.y / 2;
        } else {
            pivot.y = 0;
            position.y = handler.position.y + handler.rect.size.y * handler.lossyScale.y / 2;
        }

        _parent.pivot = pivot;
        _parent.position = position;
    }       
}