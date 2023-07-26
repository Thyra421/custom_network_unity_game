﻿using System;

public enum DamageType
{
    Physical, Magic
}

public interface IDirectEffectController
{
    public void ApplyAlteration(Alteration alteration);

    public void DealDamage(int amount, DamageType damageType);

    public void RestoreHealth(int amount);
}

[Serializable]
public class DirectEffect : Effect
{
    public DirectEffect() : base(typeof(IDirectEffectController).AssemblyQualifiedName) { }
}