using UnityEngine;

[RequireComponent(typeof(LocalPlayerMovement))]
[RequireComponent(typeof(LocalPlayerAttack))]
public class LocalPlayer : NetworkObject
{
    [SerializeField]
    private LocalPlayerMovement _playerMovement;
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
                UDPClient.Send(new ClientMessageMovement(transformData, new MovementData(_playerMovement.Movement.x, _playerMovement.Movement.z)));
            }
        }
    }

    private void FixedUpdate() {
        SendMovement();
    }
}