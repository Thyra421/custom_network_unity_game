using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerAbilities))]
[RequireComponent(typeof(PlayerActivity))]
[RequireComponent(typeof(PlayerCooldowns))]
[RequireComponent(typeof(PlayerAlterations))]
[RequireComponent(typeof(PlayerStatistics))]
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
    [SerializeField]
    private PlayerStatistics _statistics;

    public Client Client { get; private set; }
    public Room Room { get; private set; }
    public PlayerInventory Inventory { get; }
    public PlayerExperience Experience { get; }

    private Player() {
        Inventory = new PlayerInventory(this);
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

    public PlayerAlterations Alterations => _alterations;

    public PlayerStatistics Statistics => _statistics;
}
