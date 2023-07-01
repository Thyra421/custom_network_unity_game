using UnityEngine;

[RequireComponent(typeof(LocalPlayerMovement))]
[RequireComponent(typeof(LocalPlayerAttack))]
public class LocalPlayer : Character
{
    [SerializeField]
    private LocalPlayerMovement _movement;
    private float _elapsedTime = 0f;
    private TransformData _lastTransform;

    private void SendMovement() {
        _elapsedTime += Time.deltaTime;
        if (_elapsedTime >= (1f / SharedConfig.SYNC_FREQUENCY)) {
            _elapsedTime = 0f;
            TransformData transformData = new TransformData(transform);
            if (!transformData.Equals(_lastTransform)) {
                _lastTransform = transformData;
                UDPClient.Send(new MessageMovement(transformData, _movement.Animation));
            }
        }
    }

    private void Update() {
        SendMovement();
    }
}