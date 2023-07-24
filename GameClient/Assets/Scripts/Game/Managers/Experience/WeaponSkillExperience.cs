public class WeaponSkillExperience
{
    public Weapon Weapon { get; }
    public PlayerSkillExperience Experience { get; } = new PlayerSkillExperience();

    public WeaponSkillExperience(Weapon weapon) {
        Weapon = weapon;
    }
}
