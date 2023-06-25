using UnityEngine;

public class Player : MonoBehaviour
{
    private readonly string _id = Utils.GenerateUUID();
    private readonly Inventory _inventory;
    private readonly ServerItemActionController _itemActionController;
    private readonly Statistics _statistics;
    private Client _client;
    private Room _room;
    private TransformData _transformData;
    private AnimationData _animationData;
    private TransformData _lastTransform;

    private Player() {
        _inventory = new Inventory(this);
        _itemActionController = new ServerItemActionController(this);
        _statistics = new Statistics(this);
    }

    private void Awake() {
        _transformData = new TransformData(transform);
    }

    public void Initialize(Client client, Room room) {
        _client = client;
        _room = room;
        client.Player = this;
    }

    public void Attack() {
        AttackHitbox attackHitbox = Instantiate(Resources.Load<GameObject>($"{Config.PREFABS_PATH}/AttackHitbox"), transform).GetComponent<AttackHitbox>();
        attackHitbox.Initialize(this);
    }

    public bool UpdateTransformIfChanged() {
        if (_lastTransform.Equals(_transformData))
            return false;
        else {
            _lastTransform = _transformData;
            return true;
        }
    }

    public void Use(UsableItem item) {
        _itemActionController.Use(item);
    }

    public string Id => _id;

    public Client Client => _client;

    public Room Room => _room;

    public Inventory Inventory => _inventory;

    public Statistics Statistics => _statistics;

    public TransformData TransformData {
        get => _transformData;
        set {
            _transformData = value;
            transform.position = value.position.ToVector3;
            transform.eulerAngles = value.rotation.ToVector3;
        }
    }
    public AnimationData AnimationData {
        get => _animationData;
        set => _animationData = value;
    }

    public PlayerData Data => new PlayerData(_id, _transformData, _animationData);   
}
