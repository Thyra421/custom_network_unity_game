using TMPro;
using UnityEngine;

[CreateAssetMenu(menuName = "Config/TooltipBuilder")]
public class TooltipBuilder : SingletonScriptableObject<TooltipBuilder>
{
    [SerializeField]
    private GameObject _tooltipTextPrefab;

    public void BuildText(RectTransform parent, string text, FontStyles style = FontStyles.Normal) {
        Instantiate(_tooltipTextPrefab, parent).GetComponent<TooltipTextGUI>().Initialize(text, style);
    }

    public void BuildText(RectTransform parent, string text, Color color, FontStyles style = FontStyles.Normal) {
        Instantiate(_tooltipTextPrefab, parent).GetComponent<TooltipTextGUI>().Initialize(text, color, style);
    }
}