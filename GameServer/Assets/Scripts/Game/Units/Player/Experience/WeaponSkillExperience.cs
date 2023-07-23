public class WeaponSkillExperience
{
    public Weapon Weapon { get; private set; }
    public PlayerSkillExperience Experience { get; private set; }

    public WeaponSkillExperience(Player player, Weapon weapon) {
        Weapon = weapon;
        Experience = new PlayerSkillExperience(player, 20, SkillType.Weapon);
    }
}
