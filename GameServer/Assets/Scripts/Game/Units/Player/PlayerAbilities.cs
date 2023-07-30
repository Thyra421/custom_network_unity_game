using System.Collections;
using System.Linq;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    [SerializeField]
    private Player _player;
    [SerializeField]
    private GameObject _meleePrefab;
    private Weapon _weapon;
    private Ability _extraAbility;

    private void Aimed(AbilityHit hit, Vector3 aimTarget, GameObject prefab, float speed, float distance) {
        GameObject obj = Instantiate(prefab, _player.transform.position + Vector3.up, Quaternion.identity);
        AttackHitbox attackHitbox = obj.AddComponent<AttackHitbox>();
        Projectile projectile = obj.AddComponent<Projectile>();
        attackHitbox.Initialize(_player, hit, -1);
        projectile.Initialize(speed, distance, aimTarget);
        _player.Room.VFXsManager.CreateVFX(obj, prefab.name, speed);
    }

    private void Melee(AbilityHit hit, float duration) {
        AttackHitbox attackHitbox = Instantiate(_player.Abilities.MeleePrefab, _player.transform).GetComponent<AttackHitbox>();
        attackHitbox.Initialize(_player, hit, duration);
    }

    private IEnumerator AOE(AbilityHit hit, Vector3 targetPosition, GameObject prefab, float durationInSeconds, float delayInSeconds) {
        yield return new WaitForSeconds(delayInSeconds);

        GameObject obj = Instantiate(prefab, targetPosition, Quaternion.identity);
        AttackHitbox attackHitbox = obj.AddComponent<AttackHitbox>();
        attackHitbox.Initialize(_player, hit, durationInSeconds);
    }

    public void UseAbility(Ability ability, Vector3 aimTarget) {
        // is allowed to use this ability?
        if ((_weapon == null || !_weapon.Abilities.Any((Ability a) => a == ability)) && ability != _extraAbility) {
            _player.Client.Tcp.Send(new MessageError(MessageErrorType.cantDoThat));
            return;
        }
        // ability is in coolown?
        if (_player.Cooldowns.Any(ability)) {
            _player.Client.Tcp.Send(new MessageError(MessageErrorType.inCooldown));
            return;
        }

        new PlayerDirectEffectController(_player, _player).Use(ability);

        if (ability is OffensiveAbility offensiveAbility) {
            if (ability is MeleeAbility meleeAbility)
                Melee(meleeAbility.Hit, meleeAbility.Duration);
            if (ability is AimedAbility aimedAbility)
                Aimed(aimedAbility.Hit, aimTarget, aimedAbility.Prefab, aimedAbility.Speed, aimedAbility.Distance);
            if (ability is AOEAbility AOEAbility)
                StartCoroutine(AOE(AOEAbility.Hit, aimTarget, AOEAbility.Prefab, AOEAbility.DurationInSeconds, AOEAbility.DelayInSeconds));
        }

        _player.Client.Tcp.Send(new MessageUsedAbility(ability.name));
        _player.Room.PlayersManager.BroadcastTCP(new MessageTriggerAnimation(_player.Id, ability.AnimationName));
    }

    public void Equip(Weapon weapon) {
        if (_player.Inventory.Contains(weapon, 1)) {
            _weapon = weapon;
            _player.Room.PlayersManager.BroadcastTCP(new MessageEquiped(_player.Id, _weapon.name));
        } else
            _player.Client.Tcp.Send(new MessageError(MessageErrorType.cantDoThat));
    }

    public GameObject MeleePrefab => _meleePrefab;
}