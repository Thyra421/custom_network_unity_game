using System;

public interface IStatusEffectController
{
    public void ModifyStatistic(StatisticType type, float value, bool percent);

    public void Root();
}

[Serializable]
public class StatusEffect : Effect
{
    public StatusEffect() : base(typeof(IStatusEffectController).AssemblyQualifiedName) { }
}