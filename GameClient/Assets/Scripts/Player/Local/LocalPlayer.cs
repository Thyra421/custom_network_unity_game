using UnityEngine;

[RequireComponent(typeof(LocalPlayerMovement))]
[RequireComponent(typeof(LocalPlayerAttack))]
public class LocalPlayer : Player
{
    private readonly Inventory _inventory = new Inventory();
    [SerializeField]
    private LocalPlayerMovement _movement;
    private float _elapsedTime = 0f;
    private TransformData _lastTransform;

    private void SendMovement() {
        _elapsedTime += Time.deltaTime;
        if (_elapsedTime >= (1f / Config.SyncFrequency)) {
            _elapsedTime = 0f;
            TransformData transformData = new TransformData(transform);
            if (!transformData.Equals(_lastTransform)) {
                _lastTransform = transformData;
                UDPClient.Send(new MessageMovement(transformData, new AnimationData(_movement.Movement.x, _movement.Movement.z)));
            }
        }
    }

    private void Update() {
        SendMovement();
    }

    public Inventory Inventory => _inventory;
}