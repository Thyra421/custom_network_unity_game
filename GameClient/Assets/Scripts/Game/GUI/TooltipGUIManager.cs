using UnityEngine;
using UnityEngine.UI;

public class TooltipGUIManager : MonoBehaviour
{
    [SerializeField]
    private RectTransform _parent;
    [SerializeField]
    private VerticalLayoutGroup _verticalLayoutGroup;
    private ITooltipHandlerGUI _currentTooltip;

    public static TooltipGUIManager Current { get; private set; }

    private void Clear() {
        for (int i = 0; i < _parent.childCount; i++)
            Destroy(_parent.GetChild(i).gameObject);
    }

    private void Build() {
        _currentTooltip.BuildTooltip(_parent);
        LayoutRebuilder.ForceRebuildLayoutImmediate(_parent);
    }

    private void Rebuild() {
        Clear();
        Build();
    }

    private void LateUpdate() {
        _parent.position = Input.mousePosition;
    }

    private void Awake() {
        if (Current == null)
            Current = this;
        else
            Destroy(gameObject);
    }

    public ITooltipHandlerGUI Tooltip {
        get => _currentTooltip;
        set {
            if (value != _currentTooltip) {
                _currentTooltip = value;
                if (_currentTooltip != null) {
                    _parent.gameObject.SetActive(true);
                    Rebuild();
                } else
                    _parent.gameObject.SetActive(false);
            }
        }
    }
}