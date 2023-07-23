using System.Collections.Generic;

public class PlayerExperience
{
    private readonly Player _player;
    private readonly PlayerSkillExperience _generalExperience;
    private readonly PlayerSkillExperience _gatheringExperience;
    private readonly PlayerSkillExperience _miningExperience;
    private readonly PlayerSkillExperience _cookingExperience;
    private readonly PlayerSkillExperience _alchemyExperience;
    private readonly PlayerSkillExperience _forgingExperience;
    private readonly PlayerSkillExperience _lumberjackingExperience;
    private readonly PlayerSkillExperience _engineeringExperience;
    private readonly PlayerSkillExperience _huntingExperience;
    private readonly List<WeaponSkillExperience> _weaponsExperience = new List<WeaponSkillExperience>();

    public PlayerExperience(Player player) {
        _player = player;
        _generalExperience = new PlayerSkillExperience(player, 60, SkillType.General);
        _gatheringExperience = new PlayerSkillExperience(_player, 20, SkillType.Gathering);
        _miningExperience = new PlayerSkillExperience(_player, 20, SkillType.Mining);
        _cookingExperience = new PlayerSkillExperience(_player, 20, SkillType.Cooking);
        _alchemyExperience = new PlayerSkillExperience(_player, 20, SkillType.Alchemy);
        _forgingExperience = new PlayerSkillExperience(_player, 20, SkillType.Forging);
        _lumberjackingExperience = new PlayerSkillExperience(_player, 20, SkillType.Lumberjacking);
        _engineeringExperience = new PlayerSkillExperience(_player, 20, SkillType.Engineering);
        _huntingExperience = new PlayerSkillExperience(_player, 20, SkillType.Hunting);
    }

    public void AddWeaponExperience(Weapon weapon, int amount) {
        WeaponSkillExperience skillExperience = FindWeaponExperience(weapon);

        if (skillExperience == null) {
            skillExperience = new WeaponSkillExperience(_player, weapon);
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
