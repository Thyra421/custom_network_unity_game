public class PlayerSkillExperience
{
    private float _currentRatio;
    private int _currentLevel = 1;

    public delegate void OnChangedHandler(float currentRatio, int currentLevel);
    public event OnChangedHandler OnChanged;

    public void SetExperience(int currentLevel, float currentRatio) {
        _currentLevel = currentLevel;
        _currentRatio = currentRatio;
        OnChanged?.Invoke(_currentRatio, _currentLevel);
    }
}
