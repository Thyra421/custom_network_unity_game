using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilitySlotGUI : MonoBehaviour, ITooltipHandlerGUI
{
    [SerializeField]
    private RectTransform _transform;
    [SerializeField]
    private Image _image;
    [SerializeField]
    private Image _swipe;
    [SerializeField]
    private TMP_Text _text;
    private AbilitySlot _slot;

    private void OnChanged(Ability ability) {
        if (ability != null) {
            _image.gameObject.SetActive(true);
            _image.sprite = ability.Icon;
        } else
            _image.gameObject.SetActive(false);
    }

    private void OnUpdated(float cooldown) {
        _text.text = Mathf.CeilToInt(cooldown).ToString();
        _swipe.fillAmount = cooldown / _slot.CurrentAbility.CooldownInSeconds;
        _text.gameObject.SetActive(cooldown > 0);
    }

    public void Initialize(AbilitySlot slot) {
        _slot = slot;
        _slot.OnChanged += OnChanged;
        _slot.OnUpdated += OnUpdated;
    }

    public void Use() {
        _slot.Use();
    }

    public void BuildTooltip(RectTransform parent) {
        _slot.CurrentAbility.BuildTooltip(parent);
    }

    public bool IsTooltipReady => _slot?.CurrentAbility != null;

    public RectTransform RectTransform => _transform;
}