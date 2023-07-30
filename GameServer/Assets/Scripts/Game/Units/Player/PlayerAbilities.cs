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

    private void Aimed(AimedAbility aimedAbility, Vector3 aimTarget) {
        GameObject obj = Instantiate(aimedAbility.Prefab, _player.transform.position + Vector3.up, Quaternion.identity);
        DirectAbilityHitbox attackHitbox = obj.AddComponent<DirectAbilityHitbox>();
        Projectile projectile = obj.AddComponent<Projectile>();
        attackHitbox.Initialize(_player, aimedAbility.Hit, -1);
        projectile.Initialize(aimedAbility.Speed, aimedAbility.Distance, aimTarget);
        _player.Room.VFXsManager.CreateVFX(obj, aimedAbility.Prefab.name, aimedAbility.Speed);
    }

    private void Melee(MeleeAbility meleeAbility) {
        DirectAbilityHitbox attackHitbox = Instantiate(_player.Abilities.MeleePrefab, _player.transform).GetComponent<DirectAbilityHitbox>();
        attackHitbox.Initialize(_player, meleeAbility.Hit, meleeAbility.DurationInSeconds);
    }

    private IEnumerator DirectAOE(DirectAOEAbility directAOEAbility, Vector3 targetPosition) {
        yield return new WaitForSeconds(directAOEAbility.DelayInSeconds);

        GameObject obj = Instantiate(directAOEAbility.Prefab, targetPosition, Quaternion.identity);
        DirectAbilityHitbox attackHitbox = obj.AddComponent<DirectAbilityHitbox>();
        attackHitbox.Initialize(_player, directAOEAbility.Hit, directAOEAbility.DurationInSeconds);
        _player.Room.VFXsManager.CreateVFX(obj, directAOEAbility.Prefab.name, 0);
    }

    private IEnumerator PersistentAOE(PersistentAOEAbility persistentAOEAbility, Vector3 targetPosition) {
        yield return new WaitForSeconds(persistentAOEAbility.DelayInSeconds);

        GameObject obj = Instantiate(persistentAOEAbility.Prefab, targetPosition, Quaternion.identity);
        PersistentAbilityHitbox attackHitbox = obj.AddComponent<PersistentAbilityHitbox>();
        attackHitbox.Initialize(_player, persistentAOEAbility.Alteration, persistentAOEAbility.DurationInSeconds);
        _player.Room.VFXsManager.CreateVFX(obj, persistentAOEAbility.Prefab.name, 0);
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

        if (ability is DirectAbility directAbility) {
            if (directAbility is MeleeAbility meleeAbility)
                Melee(meleeAbility);
            else if (directAbility is AimedAbility aimedAbility)
                Aimed(aimedAbility, aimTarget);
            else if (directAbility is DirectAOEAbility directAOEAbility)
                StartCoroutine(DirectAOE(directAOEAbility, aimTarget));
        } else if (ability is PersistentAOEAbility persistentAOEAbility)
            StartCoroutine(PersistentAOE(persistentAOEAbility, aimTarget));

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