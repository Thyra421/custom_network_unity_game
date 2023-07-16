using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerAbilities))]
public class Player : Unit
{
    private readonly PlayerInventory _inventory;
    private readonly PlayerItemActionController _itemActionController;
    private readonly PlayerExperience _experience;
    private readonly PlayerStatistics _statistics = new PlayerStatistics();
    [SerializeField]
    private PlayerMovement _movement;
    [SerializeField]
    private PlayerAbilities _abilities;
    [SerializeField]
    private PlayerActivity _activity;
    [SerializeField]
    private PlayerCooldowns _cooldowns;
    private Client _client;
    private Room _room;

    private Player() {
        _inventory = new PlayerInventory(this);
        _itemActionController = new PlayerItemActionController(this);
        _experience = new PlayerExperience(this);
    }

    public void Initialize(Client client, Room room) {
        _client = client;
        _room = room;
        client.Player = this;
    }

    public Client Client => _client;

    public Room Room => _room;

    public PlayerInventory Inventory => _inventory;

    public PlayerStatistics Statistics => _statistics;

    public PlayerItemActionController ItemActionController => _itemActionController;

    public PlayerAbilities Abilities => _abilities;

    public PlayerMovement Movement => _movement;

    public PlayerActivity Activity => _activity;

    public PlayerExperience Experience => _experience;

    public PlayerCooldowns Cooldowns => _cooldowns;

    public PlayerData Data => new PlayerData(_id, Movement.TransformData, Movement.PlayerAnimationData);
}
