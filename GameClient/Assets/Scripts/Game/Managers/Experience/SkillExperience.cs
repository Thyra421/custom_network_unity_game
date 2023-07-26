public class SkillExperience
{
    private float _currentRatio;
    private int _currentLevel = 1;

    public ExperienceType ExperienceType { get; }

    public delegate void OnChangedHandler(float currentRatio, int currentLevel);
    public event OnChangedHandler OnChanged;

    public SkillExperience(ExperienceType experienceType) {
        ExperienceType = experienceType;
    }

    public void SetExperience(int currentLevel, float currentRatio) {
        _currentLevel = currentLevel;
        _currentRatio = currentRatio;
        OnChanged?.Invoke(_currentRatio, _currentLevel);
    }
}
