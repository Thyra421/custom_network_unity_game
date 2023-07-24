using System;

public interface IDirectEffectController
{
    public void ApplyAlteration(Alteration alteration);

    public void DealDamage(int amount);

    public void RestoreHealth(int amount);
}

[Serializable]
public class DirectEffect : Effect
{
    public DirectEffect() : base(typeof(IDirectEffectController).AssemblyQualifiedName) { }
}