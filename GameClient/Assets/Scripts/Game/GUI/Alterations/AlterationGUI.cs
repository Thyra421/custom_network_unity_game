using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AlterationGUI : MonoBehaviour, ITooltipHandlerGUI
{
    [SerializeField]
    private RectTransform _rectTransform;
    [SerializeField]
    private Image _image;
    [SerializeField]
    private TMP_Text _timerText;

    public AlterationController AlterationController { get; private set; }

    private void OnRemainingDurationChanged(float remainingDuration) {
        _timerText.text = Mathf.Ceil(remainingDuration).ToString();
    }

    public void Initialize(AlterationController alterationController) {
        AlterationController = alterationController;
        _image.sprite = alterationController.Alteration.Icon;
        _timerText.text = alterationController.RemainingDuration.ToString();
        _timerText.gameObject.SetActive(!alterationController.Alteration.IsPermanent);
        AlterationController.OnRemainingDurationChanged += OnRemainingDurationChanged;
    }   

    public void BuildTooltip(RectTransform parent) {
        AlterationController.Alteration.BuildTooltip(parent);
    }

    public RectTransform RectTransform => _rectTransform;

    public bool IsTooltipReady => AlterationController != null && AlterationController.Alteration != null;
}