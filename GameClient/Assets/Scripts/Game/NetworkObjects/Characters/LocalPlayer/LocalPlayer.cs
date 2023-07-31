using UnityEngine;

[RequireComponent(typeof(LocalPlayerMovement))]
public class LocalPlayer : Character
{
    [SerializeField]
    private LocalPlayerMovement _movement;
    [SerializeField]
    private LocalPlayerAnimation _animation;
    private float _elapsedTime = 0f;
    private TransformData _lastTransform;

    public LocalPlayerAnimation Animation => _animation;
    public LocalPlayerMovement Movement => _movement;

    private void SendMovement() {
        TransformData transformData = new TransformData(transform);

        if (!transformData.Equals(_lastTransform)) {
            _lastTransform = transformData;
            UDPClient.Send(new MessageMovement(transformData, Animation.Data));
        }
    }

    private void Update() {
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime >= (1f / SharedConfig.Current.SyncFrequency)) {
            _elapsedTime = 0f;
            SendMovement();
        }
    }
}