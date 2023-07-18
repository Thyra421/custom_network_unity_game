using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerAbilities))]
public class Player : Unit
{
    [SerializeField]
    private PlayerMovement _movement;
    [SerializeField]
    private PlayerAbilities _abilities;
    [SerializeField]
    private PlayerActivity _activity;
    [SerializeField]
    private PlayerCooldowns _cooldowns;

    public PlayerStatistics Statistics { get; } = new PlayerStatistics();
    public Client Client { get; private set; }
    public Room Room { get; private set; }
    public PlayerInventory Inventory { get; }
    public PlayerItemEffectController ItemEffectController { get; }
    public PlayerExperience Experience { get; }

    private Player() {
        Inventory = new PlayerInventory(this);
        ItemEffectController = new PlayerItemEffectController(this);
        Experience = new PlayerExperience(this);
    }

    public void Initialize(Client client, Room room) {
        Client = client;
        Room = room;
        client.Player = this;
    }

    public PlayerAbilities Abilities => _abilities;

    public PlayerMovement Movement => _movement;

    public PlayerActivity Activity => _activity;

    public PlayerCooldowns Cooldowns => _cooldowns;

    public PlayerData Data => new PlayerData(Id, Movement.TransformData, Movement.AnimationData);
}
