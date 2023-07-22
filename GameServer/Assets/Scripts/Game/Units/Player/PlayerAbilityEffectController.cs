using System.Linq;
using UnityEngine;

public class PlayerAbilityEffectController : IAbilityEffectController
{
    private readonly Player _player;
    private Vector3 _aimTarget;

    public PlayerAbilityEffectController(Player player) {
        _player = player;
    }

    public void Use(Ability ability, Vector3Data aimTarget) {
        _aimTarget = aimTarget.ToVector3;
        foreach (AbilityEffect effect in ability.Effects) {
            typeof(PlayerAbilityEffectController).GetMethod(effect.MethodName).Invoke(this, effect.Parameters.Select((EffectParameter param) => param.ToObject).ToArray());
        }
    }

    public void Melee(int damage, string animationName, float duration) {
        AttackHitbox attackHitbox = Object.Instantiate(_player.Abilities.MeleePrefab, _player.transform).GetComponent<AttackHitbox>();
        attackHitbox.Initialize(_player, damage, duration);
        _player.Room.PlayersManager.BroadcastTCP(new MessageTriggerAnimation(_player.Id, animationName));
    }

    public void Projectile(int damage, string animationName, GameObject prefab, float speed, float distance) {
        GameObject obj = Object.Instantiate(prefab, _player.transform.position + Vector3.up, Quaternion.identity);
        AttackHitbox attackHitbox = obj.AddComponent<AttackHitbox>();
        Projectile projectile = obj.AddComponent<Projectile>();
        attackHitbox.Initialize(_player, damage, -1);
        projectile.Initialize(speed, distance, _aimTarget);
        _player.Room.VFXsManager.CreateVFX(obj, prefab.name, speed);
        _player.Room.PlayersManager.BroadcastTCP(new MessageTriggerAnimation(_player.Id, animationName));
    }
}