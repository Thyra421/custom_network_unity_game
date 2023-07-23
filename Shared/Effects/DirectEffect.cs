using System;

public interface IDirectEffectController
{
    public void ApplyStatusModifier(StatusModifier modifier);

    public void DealDamage(int amount);

    public void RestoreHealth(int amount);
}

[Serializable]
public class DirectEffect : Effect
{
    public DirectEffect() : base(typeof(IDirectEffectController).AssemblyQualifiedName) { }
}