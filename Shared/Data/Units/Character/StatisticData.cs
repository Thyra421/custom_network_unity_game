public enum StatisticType
{
    MovementSpeed,
    JumpHeight,
    PhysicalDamage,
    MagicDamage,
    PhysicalArmor,
    MagicArmor,
    Healing,
    GatheringSpeed,
    MiningSpeed,
    SkinningSpeed,
    LumberjackingSpeed
}

public struct StatisticData
{
    public StatisticType type;
    public float value;

    public StatisticData(StatisticType type, float value) {
        this.type = type;
        this.value = value;
    }
}