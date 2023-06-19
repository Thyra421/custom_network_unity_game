using UnityEngine;

public class Player : MonoBehaviour
{
    private string _id;
    private Client _client;
    private Room _room;
    private TransformData _transformData;
    private AnimationData _animationData;
    private TransformData _lastTransform;

    public void Initialize(Client client, Room room) {
        _id = Utils.GenerateUUID();
        _client = client;
        _room = room;
        client.Player = this;
    }

    public void Attack() {
        AttackHitbox attackHitbox = Instantiate(Resources.Load<GameObject>("Prefabs/AttackHitbox"), transform).GetComponent<AttackHitbox>();
        attackHitbox.Player = this;
    }

    public bool UpdateTransformIfChanged() {
        TransformData transformData = new TransformData(transform);
        if (_lastTransform.Equals(transformData))
            return false;
        else {
            _lastTransform = transformData;
            return true;
        }
    }

    public string Id => _id;

    public Client Client => _client;

    public Room Room => _room;

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
