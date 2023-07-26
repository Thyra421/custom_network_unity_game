using UnityEngine;

public class SkillExperience
{
    private int _currentExperience;
    private int _experienceToLevel = 20;
    private int _currentLevel = 1;

    public ExperienceType ExperienceType { get; }

    private float Ratio => (float)_currentExperience / _experienceToLevel;

    private void LevelUp() {
        _currentLevel++;
        _currentExperience = 0;
        _experienceToLevel = Mathf.RoundToInt(_experienceToLevel * Config.LEVEL_EXPERIENCE_INCREASE_MULTIPLICATOR);
    }

    public SkillExperience(ExperienceType experienceType) {
        ExperienceType = experienceType;
    }

    public void AddExperience(int amount) {
        _currentExperience += amount;
        if (_currentExperience >= _experienceToLevel) {
            int excess = _currentExperience - _experienceToLevel;
            LevelUp();
            AddExperience(excess);
        }
    }

    public ExperienceData Data => new ExperienceData(ExperienceType, _currentLevel, Ratio);
}
