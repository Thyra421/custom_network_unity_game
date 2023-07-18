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

    public void Initialize(string skillName, PlayerSkillExperience skillExperience) {
        _levelText.text = 0.ToString();
        _ratioSlider.value = 0;
        _skillNameText.text = skillName;
        skillExperience.OnChanged += OnChanged;
    }
}