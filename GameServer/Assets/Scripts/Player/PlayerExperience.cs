using System.Collections.Generic;
using UnityEngine;

public class PlayerExperience
{
    private readonly PlayerSkillExperience _generalExperience = new PlayerSkillExperience(60);
    private readonly PlayerSkillExperience _gatheringExperience = new PlayerSkillExperience(20);
    private readonly PlayerSkillExperience _miningExperience = new PlayerSkillExperience(20);
    private readonly PlayerSkillExperience _cookingExperience = new PlayerSkillExperience(20);
    private readonly PlayerSkillExperience _alchemyExperience = new PlayerSkillExperience(20);
    private readonly PlayerSkillExperience _forgingExperience = new PlayerSkillExperience(20);
    private readonly PlayerSkillExperience _lumberjackingExperience = new PlayerSkillExperience(20);
    private readonly PlayerSkillExperience _engineeringExperience = new PlayerSkillExperience(20);
    private readonly PlayerSkillExperience _huntingExperience = new PlayerSkillExperience(20);
    private readonly List<WeaponSkillExperience> _weaponsExperience = new List<WeaponSkillExperience>();

    public class WeaponSkillExperience
    {
        private readonly Weapon _weapon;
        private readonly PlayerSkillExperience _experience = new PlayerSkillExperience(20);

        public WeaponSkillExperience(Weapon weapon) {
            _weapon = weapon;
        }

        public Weapon Weapon => _weapon;

        public PlayerSkillExperience Experience => _experience;
    }

    public class PlayerSkillExperience
    {
        private int _currentExperience;
        private int _experienceToLevel;
        private int _currentLevel = 1;

        private void LevelUp() {
            _currentLevel++;
            _currentExperience = 0;
            _experienceToLevel = Mathf.RoundToInt(_experienceToLevel * 1.3f);
        }

        public void AddExperience(int amount) {
            _currentExperience += amount;
            if (_currentExperience > _experienceToLevel) {
                int excess = _currentExperience - _experienceToLevel;
                LevelUp();
                AddExperience(excess);
            }
        }

        public PlayerSkillExperience(int experienceToLevel) {
            _experienceToLevel = experienceToLevel;
        }

        public float Ratio => (float)_currentExperience / _experienceToLevel;

        public int CurrentLevel => _currentLevel;
    }

    public void AddWeaponExperience(Weapon weapon, int amount) {
        WeaponSkillExperience skillExperience = FindWeaponExperience(weapon);

        if (skillExperience == null) {
            skillExperience = new WeaponSkillExperience(weapon);
            _weaponsExperience.Add(skillExperience);
        }

        skillExperience.Experience.AddExperience(amount);
    }

    public PlayerSkillExperience GetSkillExperience(SkillType skillType) {
        switch (skillType) {
            case SkillType.General:
                return _generalExperience;
            case SkillType.Gathering:
                return _gatheringExperience;
            case SkillType.Mining:
                return _miningExperience;
            case SkillType.Lumberjacking:
                return _lumberjackingExperience;
            case SkillType.Hunting:
                return _huntingExperience;
            case SkillType.Cooking:
                return _cookingExperience;
            case SkillType.Alchemy:
                return _alchemyExperience;
            case SkillType.Forging:
                return _forgingExperience;
            case SkillType.Engineering:
                return _engineeringExperience;
        }
        return null;
    }

    public WeaponSkillExperience FindWeaponExperience(Weapon weapon) => _weaponsExperience.Find((WeaponSkillExperience w) => w.Weapon == weapon);
}
