public readonly struct StatisticModifier
{
    public float Value { get; }
    public bool Percent { get; }

    public StatisticModifier(float value, bool percent) {
        Value = value;
        Percent = percent;
    }
}