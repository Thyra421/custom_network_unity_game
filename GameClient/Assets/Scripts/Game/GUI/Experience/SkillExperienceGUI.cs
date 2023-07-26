using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillExperienceGUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _skillNameText;
    [SerializeField]
    private TMP_Text _levelText;
    [SerializeField]
    private Slider _ratioSlider;

    private void OnChanged(float currentRatio, int currentLevel) {
        _levelText.text = currentLevel.ToString();
        _ratioSlider.value = currentRatio;
    }

    public void Initialize(SkillExperience skillExperience) {
        _levelText.text = 1.ToString();
        _ratioSlider.value = 0;
        _skillNameText.text = skillExperience.ExperienceType.ToString();
        skillExperience.OnChanged += OnChanged;
    }
}