using System.Linq;
using UnityEngine;

public class CharacterDirectEffectController : IDirectEffectController
{
    private readonly Character _target;
    private readonly Character _owner;

    public CharacterDirectEffectController(Character target, Character owner) {
        _target = target;
        _owner = owner;
    }

    public void Use(IUsable usable) {
        foreach (Effect effect in usable.Effects)
            typeof(CharacterDirectEffectController).GetMethod(effect.MethodName).Invoke(this, effect.Parameters.Select((EffectParameter param) => param.ToObject).ToArray());
    }

    public void ApplyAlteration(Alteration alteration) {
        _target.Alterations.Apply(alteration, _owner);
    }

    public void RestoreHealth(int amount) {
        _target.Health.CurrentHealth += Mathf.FloorToInt(amount * _owner.Statistics.Find(StatisticType.Healing).AlteredValue);
    }

    public void DealDamage(int amount, DamageType damageType) {
        switch (damageType) {
            case DamageType.Physical:
                _target.Health.CurrentHealth -= Mathf.FloorToInt(amount * _owner.Statistics.Find(StatisticType.PhysicalDamage).AlteredValue);
                break;
            case DamageType.Magic:
                _target.Health.CurrentHealth -= Mathf.FloorToInt(amount * _owner.Statistics.Find(StatisticType.MagicDamage).AlteredValue);
                break;
        }
    }
}