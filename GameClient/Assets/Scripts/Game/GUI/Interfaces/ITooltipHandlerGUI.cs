using UnityEngine;

public interface ITooltipHandlerGUI
{
    public RectTransform RectTransform { get; }
    public bool IsTooltipReady { get; }

    public void BuildTooltip(RectTransform parent);
}