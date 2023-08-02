using System;

public interface IPersistentEffectController
{
    public void ModifyStatistic(StatisticType type, float value, bool percent);

    public void StateStack(StateType type);
}

[Serializable]
public class StatusEffect : Effect
{
    public StatusEffect() : base(typeof(IPersistentEffectController).AssemblyQualifiedName) { }
}