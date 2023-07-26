public enum ExperienceType
{
    General,
    Gathering,
    Mining,
    Lumberjacking,
    Hunting,
    Cooking,
    Alchemy,
    Forging,
    Engineering
}

public struct ExperienceData
{
    public ExperienceType type;
    public int level;
    public float ratio;

    public ExperienceData(ExperienceType type, int level, float ratio) {
        this.type = type;
        this.level = level;
        this.ratio = ratio;
    }
}