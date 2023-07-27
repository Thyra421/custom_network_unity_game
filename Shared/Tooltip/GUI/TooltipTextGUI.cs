using TMPro;
using UnityEngine;

public class TooltipTextGUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _text;

    public void Initialize(string text, FontStyles style = FontStyles.Normal) {
        _text.text = text;
        _text.fontStyle = style;
    }

    public void Initialize(string text, Color color, FontStyles style = FontStyles.Normal) {
        _text.text = text;
        _text.color = color;
        _text.fontStyle = style;
    }
}
