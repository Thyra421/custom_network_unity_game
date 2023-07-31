using UnityEngine;

public class SkillExperience
{
    private int _currentExperience;
    private int _experienceToLevel = 20;
    private int _currentLevel = 1;

    private float Ratio => (float)_currentExperience / _experienceToLevel;
    public ExperienceType ExperienceType { get; }
    public ExperienceData Data => new ExperienceData(ExperienceType, _currentLevel, Ratio);

    private void LevelUp() {
        _currentLevel++;
        _currentExperience = 0;
        _experienceToLevel = Mathf.RoundToInt(_experienceToLevel * Config.Current.LevelExperienceIncreaseMultiplicator);
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

}
