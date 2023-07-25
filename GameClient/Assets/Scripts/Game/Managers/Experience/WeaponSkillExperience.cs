public class WeaponSkillExperience
{
    public Weapon Weapon { get; }
    public SkillExperience Experience { get; } = new SkillExperience();

    public WeaponSkillExperience(Weapon weapon) {
        Weapon = weapon;
    }
}
