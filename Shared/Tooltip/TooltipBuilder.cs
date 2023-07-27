using TMPro;
using UnityEngine;

public static class TooltipBuilder
{
    private const string TOOLTIP_TEXT_PATH = "Shared/Tooltip/TooltipTextGUI";

    public static void BuildText(RectTransform parent, string text, FontStyles style = FontStyles.Normal) {
        GameObject prefab = Resources.Load<GameObject>(TOOLTIP_TEXT_PATH);
        Object.Instantiate(prefab, parent).GetComponent<TooltipTextGUI>().Initialize(text, style);
    }

    public static void BuildText(RectTransform parent, string text, Color color, FontStyles style = FontStyles.Normal) {
        GameObject prefab = Resources.Load<GameObject>(TOOLTIP_TEXT_PATH);
        Object.Instantiate(prefab, parent).GetComponent<TooltipTextGUI>().Initialize(text, color, style);
    }
}