public enum StatisticType
{
    movementSpeed, jumpHeight, physicalDamage, magicDamage, physicalArmor, magicArmor, healing
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