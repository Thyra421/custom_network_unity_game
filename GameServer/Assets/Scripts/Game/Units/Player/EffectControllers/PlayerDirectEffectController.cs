using System.Linq;
using UnityEngine;

public class PlayerDirectEffectController : IDirectEffectController
{
    private readonly Player _target;
    private readonly Player _owner;

    public PlayerDirectEffectController(Player target, Player owner) {
        _target = target;
        _owner = owner;
    }

    public void Use(IUsable usable) {
        foreach (Effect effect in usable.Effects)
            typeof(PlayerDirectEffectController).GetMethod(effect.MethodName).Invoke(this, effect.Parameters.Select((EffectParameter param) => param.ToObject).ToArray());
    }

    public void ApplyAlteration(Alteration alteration) {
        _target.Alterations.Apply(alteration, _owner);
    }

    public void RestoreHealth(int amount) {
        _target.Statistics.CurrentHealth += Mathf.FloorToInt(amount * _owner.Statistics.Find(StatisticType.Healing).AlteredValue);
    }

    public void DealDamage(int amount, DamageType damageType) {
        switch (damageType) {
            case DamageType.Physical:
                _target.Statistics.CurrentHealth -= Mathf.FloorToInt(amount * _owner.Statistics.Find(StatisticType.PhysicalDamage).AlteredValue);
                break;
            case DamageType.Magic:
                _target.Statistics.CurrentHealth -= Mathf.FloorToInt(amount * _owner.Statistics.Find(StatisticType.MagicDamage).AlteredValue);
                break;
        }
    }
}