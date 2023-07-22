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

    public void UseAbility(Ability ability, Vector3Data aimTarget) {
        if ((_weapon != null && _weapon.Abilities.Any((Ability a) => a == ability)) || ability == _extraAbility) {
            if (!_player.Cooldowns.Any(ability)) {
                _player.EffectController.Use(ability, aimTarget);
                _player.Client.Tcp.Send(new MessageUsedAbility(ability.name));
            } else
                _player.Client.Tcp.Send(new MessageError(MessageErrorType.inCooldown));
        } else
            _player.Client.Tcp.Send(new MessageError(MessageErrorType.cantDoThat));
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