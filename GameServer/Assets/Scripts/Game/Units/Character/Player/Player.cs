using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerAbilities))]
[RequireComponent(typeof(PlayerActivity))]
[RequireComponent(typeof(PlayerCooldowns))]
public class Player : Character
{
    [SerializeField]
    private PlayerMovement _movement;
    [SerializeField]
    private PlayerAbilities _abilities;
    [SerializeField]
    private PlayerActivity _activity;
    [SerializeField]
    private PlayerCooldowns _cooldowns;
    private PlayerHealth _health;

    public Client Client { get; private set; }
    public PlayerInventory Inventory { get; private set; }
    public PlayerExperience Experience { get; private set; }
    public PlayerAnimation Animation { get; private set; }
    public PlayerAbilities Abilities => _abilities;
    public PlayerMovement Movement => _movement;
    public PlayerActivity Activity => _activity;
    public PlayerCooldowns Cooldowns => _cooldowns;
    public PlayerData Data => new PlayerData(Id, TransformData, Animation.Data);
    public override CharacterHealth Health => _health;
    public override CharacterData CharacterData => new CharacterData(Id, CharacterType.Player);

    protected override void Awake() {
        base.Awake();
        Inventory = new PlayerInventory(this);
        Experience = new PlayerExperience(this);
        Animation = new PlayerAnimation();
        _health = new PlayerHealth(this);
    }

    public void Initialize(Room room, Client client) {
        Initialize(room);
        Client = client;
    }
}
