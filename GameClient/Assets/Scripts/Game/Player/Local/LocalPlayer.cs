using UnityEngine;

[RequireComponent(typeof(LocalPlayerMovement))]
[RequireComponent(typeof(LocalPlayerAttack))]
[RequireComponent(typeof(LocalPlayerStatistics))]
public class LocalPlayer : Player
{
    [SerializeField]
    private LocalPlayerMovement _movement;
    [SerializeField]
    private LocalPlayerStatistics _statistics;
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

    public void Initialize(string id) {
        _id = id;
    }

    public override PlayerStatistics Statistics => _statistics;
}