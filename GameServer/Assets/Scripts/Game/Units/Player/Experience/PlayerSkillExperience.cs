using UnityEngine;

public class PlayerSkillExperience
{
    private readonly Player _player;
    private readonly SkillType _skillType;
    private int _currentExperience;
    private int _experienceToLevel;

    public int CurrentLevel { get; private set; } = 1;

    private void LevelUp() {
        CurrentLevel++;
        _currentExperience = 0;
        _experienceToLevel = Mathf.RoundToInt(_experienceToLevel * Config.LEVEL_EXPERIENCE_INCREASE_MULTIPLICATOR);
    }

    public void AddExperience(int amount) {
        _currentExperience += amount;
        if (_currentExperience > _experienceToLevel) {
            int excess = _currentExperience - _experienceToLevel;
            LevelUp();
            AddExperience(excess);
            return;
        }
        _player.Client.Tcp.Send(new MessageExperienceChanged(_skillType, CurrentLevel, Ratio));
    }

    public PlayerSkillExperience(Player player, int experienceToLevel, SkillType skillType) {
        _player = player;
        _experienceToLevel = experienceToLevel;
        _skillType = skillType;
    }

    public float Ratio => (float)_currentExperience / _experienceToLevel;
}
