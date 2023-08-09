using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class CharacterDirectEffectController : IDirectEffectController
{
    private readonly Character _target;
    private Character _owner;

    public CharacterDirectEffectController(Character target) {
        _target = target;
    }

    public void Use(IUsable usable, Character owner) {
        _owner = owner;
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

    public void Dash(float distance, float speed) {
        Vector3 rawDestination = _owner.transform.position + _owner.transform.forward * distance;

        if (NavMesh.SamplePosition(rawDestination, out NavMeshHit hit, distance, NavMesh.AllAreas))
            if (_owner is Player player)
                player.Send(new MessageDash(new Vector3Data(hit.position), speed));
    }
}