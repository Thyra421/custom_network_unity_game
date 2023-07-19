using System.Linq;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    [SerializeField]
    private Player _player;
    [SerializeField]
    private GameObject _meleePrefab;
    [SerializeField]
    private GameObject _projectilePrefab;
    private PlayerAbilityEffectController _weaponAbilityEffectController;
    private Weapon _weapon;
    private Ability _extraAbility;

    private void Awake() {
        _weaponAbilityEffectController = new PlayerAbilityEffectController(_player);
    }

    public void UseAbility(Ability ability) {
        if ((_weapon != null && _weapon.Abilities.Any((Ability a) => a == ability)) || ability == _extraAbility) {
            if (!_player.Cooldowns.Any(ability)) {
                _weaponAbilityEffectController.Use(ability);
                _player.Room.PlayersManager.BroadcastTCP(new MessageUsedAbility(_player.Id, ability.name));
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