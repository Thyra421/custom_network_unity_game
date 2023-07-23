using System;

public interface IStatusEffectController
{
    public void Root();

    public void Slow(float amount);
}

[Serializable]
public class StatusEffect : Effect
{
    public StatusEffect() : base(typeof(IStatusEffectController).AssemblyQualifiedName) { }
}