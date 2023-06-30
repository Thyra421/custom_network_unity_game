using UnityEngine;

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(PlayerAttack))]
public class Player : MonoBehaviour
{
    private readonly string _id = Utils.GenerateUUID();
    private readonly Inventory _inventory;
    private readonly PlayerItemActionController _itemActionController;
    private readonly Statistics _statistics = new Statistics();
    private readonly PlayerExperience _experience = new PlayerExperience();
    [SerializeField]
    private Movement _movement;
    [SerializeField]
    private PlayerAttack _attack;
    [SerializeField]
    private Activity _activity;
    private Client _client;
    private Room _room;

    private Player() {
        _inventory = new Inventory(this);
        _itemActionController = new PlayerItemActionController(this);
    }

    public void Initialize(Client client, Room room) {
        _client = client;
        _room = room;
        client.Player = this;
    }

    public string Id => _id;

    public Client Client => _client;

    public Room Room => _room;

    public Inventory Inventory => _inventory;

    public Statistics Statistics => _statistics;

    public PlayerItemActionController ItemActionController => _itemActionController;

    public PlayerAttack Attack => _attack;

    public Movement Movement => _movement;

    public Activity Activity => _activity;

    public PlayerExperience Experience => _experience;

    public PlayerData Data => new PlayerData(_id, Movement.TransformData, Movement.AnimationData);
}
