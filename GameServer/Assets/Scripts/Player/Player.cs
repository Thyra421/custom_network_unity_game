using UnityEngine;

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(PlayerAttack))]
public class Player : MonoBehaviour
{
    private readonly string _id = Utils.GenerateUUID();
    private readonly Inventory _inventory;
    private readonly ServerItemActionController _itemActionController;
    private readonly Statistics _statistics;
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
        _itemActionController = new ServerItemActionController(this);
        _statistics = new Statistics();
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

    public ServerItemActionController ItemActionController => _itemActionController;

    public PlayerAttack Attack => _attack;

    public Movement Movement => _movement;

    public Activity Activity => _activity;

    public PlayerData Data => new PlayerData(_id, Movement.TransformData, Movement.AnimationData);
}
