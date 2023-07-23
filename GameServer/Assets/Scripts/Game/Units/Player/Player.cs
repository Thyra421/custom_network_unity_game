using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerAbilities))]
[RequireComponent(typeof(PlayerActivity))]
[RequireComponent(typeof(PlayerCooldowns))]
[RequireComponent(typeof(PlayerStatus))]
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
    [SerializeField]
    private PlayerStatus _status;

    public Client Client { get; private set; }
    public Room Room { get; private set; }
    public PlayerStatistics Statistics { get; }
    public PlayerInventory Inventory { get; }
    public PlayerDirectEffectController EffectController { get; }
    public PlayerExperience Experience { get; }

    private Player() {
        Statistics = new PlayerStatistics(this);
        Inventory = new PlayerInventory(this);
        EffectController = new PlayerDirectEffectController(this);
        Experience = new PlayerExperience(this);
    }

    public void Initialize(Client client, Room room) {
        Client = client;
        Room = room;
        client.Player = this;
    }

    public PlayerData Data => new PlayerData(Id, Movement.TransformData, Movement.AnimationData);

    public PlayerAbilities Abilities => _abilities;

    public PlayerMovement Movement => _movement;

    public PlayerActivity Activity => _activity;

    public PlayerCooldowns Cooldowns => _cooldowns;

    public PlayerStatus Status => _status;
}
