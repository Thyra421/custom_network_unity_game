using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerAttack))]
public class Player : MonoBehaviour
{
    private readonly string _id = Utils.GenerateUUID();
    private readonly PlayerInventory _inventory;
    private readonly PlayerItemActionController _itemActionController;
    private readonly PlayerExperience _experience;
    private readonly PlayerStatistics _statistics = new PlayerStatistics();
    [SerializeField]
    private PlayerMovement _movement;
    [SerializeField]
    private PlayerAttack _attack;
    [SerializeField]
    private PlayerActivity _activity;
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

    public string Id => _id;

    public Client Client => _client;

    public Room Room => _room;

    public PlayerInventory Inventory => _inventory;

    public PlayerStatistics Statistics => _statistics;

    public PlayerItemActionController ItemActionController => _itemActionController;

    public PlayerAttack Attack => _attack;

    public PlayerMovement Movement => _movement;

    public PlayerActivity Activity => _activity;

    public PlayerExperience Experience => _experience;

    public PlayerData Data => new PlayerData(_id, Movement.TransformData, Movement.PlayerAnimationData);
}
