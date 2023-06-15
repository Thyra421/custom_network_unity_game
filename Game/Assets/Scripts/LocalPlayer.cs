using UnityEngine;

[RequireComponent(typeof(LocalPlayerMovement))]
public class LocalPlayer : NetworkObject
{
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
                UDPClient.Send(new ClientMessageMovement(transformData));
            }
        }
    }

    private void FixedUpdate() {
        SendMovement();
    }
}