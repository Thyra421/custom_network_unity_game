using UnityEngine;

[RequireComponent(typeof(LocalPlayerMovement))]
[RequireComponent(typeof(LocalPlayerAttack))]
public class LocalPlayer : Player
{
    [SerializeField]
    private LocalPlayerMovement _movement;
    private float _elapsedTime = 0f;
    private const int _frequency = 20;
    private TransformData _lastTransform;

    private void SendMovement() {
        _elapsedTime += Time.deltaTime;
        if (_elapsedTime >= (1f / _frequency)) {
            _elapsedTime = 0f;
            TransformData transformData = new TransformData(transform);
            if (!transformData.Equals(_lastTransform)) {
                _lastTransform = transformData;
                UDPClient.Send(new MessageMovement(transformData, new MovementData(_movement.Movement.x, _movement.Movement.z)));
            }
        }
    }

    private void Start() {
        if (_movement == null)
            _movement = GetComponent<LocalPlayerMovement>();
    }

    private void Update() {
        SendMovement();
    }


}