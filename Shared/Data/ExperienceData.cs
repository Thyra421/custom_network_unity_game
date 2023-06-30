public enum SkillType
{
    General,
    Gathering,
    Mining,
    Lumberjacking,
    Hunting,
    Cooking,
    Alchemy,
    Forging,
    Engineering,
    Weapon
}

public struct ExperienceData
{
    public SkillType type;
    public int level;
    public float percent;

    public ExperienceData(SkillType type, int level, float percent) {
        this.type = type;
        this.level = level;
        this.percent = percent;
    }
}