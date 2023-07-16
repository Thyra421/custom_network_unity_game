using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilitySlotGUI : MonoBehaviour
{
    [SerializeField]
    private Button _button;
    [SerializeField]
    private Image _image;
    [SerializeField]
    private Image _swipe;
    [SerializeField]
    private TMP_Text _text;
    private AbilitySlot _slot;
    private Sprite _defaultSprite;

    private void Start() {
        _defaultSprite = _image.sprite;
    }

    private void OnChanged(Ability ability) {
        if (ability != null)
            _image.sprite = ability.Icon;
        else
            _image.sprite = _defaultSprite;
    }

    private void OnUpdated(float cooldown) {
        _text.text = Mathf.CeilToInt(cooldown).ToString();
        _swipe.fillAmount = cooldown / _slot.Ability.Cooldown;
        _text.gameObject.SetActive(cooldown > 0);
    }

    public void Initialize(AbilitySlot slot) {
        _slot = slot;
        _button.onClick.AddListener(slot.Use);
        _slot.OnChanged += OnChanged;
        _slot.OnUpdated += OnUpdated;
    }
}