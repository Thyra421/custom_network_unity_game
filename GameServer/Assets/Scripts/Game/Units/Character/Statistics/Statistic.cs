using System.Collections.Generic;

public class Statistic
{
    public StatisticType Type { get; }
    public float BaseValue { get; } = 1;
    public List<StatisticModifier> Modifiers { get; } = new List<StatisticModifier>();

    public StatisticData Data => new StatisticData(Type, AlteredValue);
    public float AlteredValue {
        get {
            float valueModifier = 0;
            float percentModifier = 0;

            foreach (StatisticModifier modifier in Modifiers) {
                if (modifier.Percent)
                    percentModifier += modifier.Value;
                else
                    valueModifier += modifier.Value;
            }
            return (100 + percentModifier) * (BaseValue + valueModifier) / 100;
        }
    }

    public Statistic(StatisticType type) {
        Type = type;
    }    
}
