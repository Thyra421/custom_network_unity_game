using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerAbilities))]
[RequireComponent(typeof(PlayerActivity))]
[RequireComponent(typeof(PlayerCooldowns))]
[RequireComponent(typeof(PlayerAlterations))]
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
    private PlayerAlterations _alterations;

    public Client Client { get; private set; }
    public Room Room { get; private set; }
    public PlayerInventory Inventory { get; private set; }
    public PlayerExperience Experience { get; private set; }
    public PlayerStatistics Statistics { get; private set; }
    public PlayerHealth Health { get; private set; }
    public PlayerAnimation Animation { get; private set; }
    public PlayerAbilities Abilities => _abilities;
    public PlayerMovement Movement => _movement;
    public PlayerActivity Activity => _activity;
    public PlayerCooldowns Cooldowns => _cooldowns;
    public PlayerAlterations Alterations => _alterations;
    public PlayerData Data => new PlayerData(Id, TransformData, Animation.Data);

    private void Awake() {
        Inventory = new PlayerInventory(this);
        Experience = new PlayerExperience(this);
        Statistics = new PlayerStatistics(this);
        Health = new PlayerHealth(this);
        Animation = new PlayerAnimation();
    }

    public void Initialize(Client client, Room room) {
        Client = client;
        Room = room;
    }    
}
