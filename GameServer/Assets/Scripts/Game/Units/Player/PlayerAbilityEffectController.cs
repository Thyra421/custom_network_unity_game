using System.Collections;
using System.Linq;
using UnityEngine;

public class PlayerAbilityEffectController : IAbilityEffectController
{
    private readonly Player _player;

    public PlayerAbilityEffectController(Player player) {
        _player = player;
    }

    public void Use(Ability ability) {
        foreach (AbilityEffect effect in ability.Effects) {
            typeof(PlayerAbilityEffectController).GetMethod(effect.MethodName).Invoke(this, effect.Parameters.Select((EffectParameter param) => param.ToObject).ToArray());
        }
    }

    public void Melee(int damage, string animationName, float duration) {
        AttackHitbox attackHitbox = Object.Instantiate(_player.Abilities.MeleePrefab, _player.transform).GetComponent<AttackHitbox>();
        attackHitbox.Initialize(_player, damage, duration);
    }

    public void Projectile(int damage, GameObject prefab, float speed, float distance) {
        GameObject obj = Object.Instantiate(_player.Abilities.ProjectilePrefab, _player.transform.position, _player.transform.rotation);
        AttackHitbox attackHitbox = obj.GetComponent<AttackHitbox>();
        Projectile projectile = obj.GetComponent<Projectile>();
        attackHitbox.Initialize(_player, damage, -1);
        projectile.Initialize(speed, distance);
    }
}