using UnityEngine;

public class RemotePlayerMovement : CharacterMovement
{
    private Vector3 _destinationPosition;
    private Vector3 _destinationRotation;
    private float _movementSpeed;

    private RemotePlayer RemotePlayer { get; set; }

    protected override void Move() {
        Vector3 direction = (_destinationPosition - RemotePlayer.transform.position).normalized;
        float distance = Vector3.Distance(RemotePlayer.transform.position, _destinationPosition);

        if (distance <= _movementSpeed * Time.deltaTime || distance > 2)
            RemotePlayer.transform.position = _destinationPosition;
        else
            RemotePlayer.transform.position += _movementSpeed * Time.deltaTime * direction;
    }

    protected override void Rotate() {
        RemotePlayer.transform.eulerAngles = _destinationRotation;
    }

    public RemotePlayerMovement(RemotePlayer remotePlayer) {
        RemotePlayer = remotePlayer;
        _destinationPosition = RemotePlayer.transform.position;
        _destinationRotation = RemotePlayer.transform.eulerAngles;
    }

    public void SetMovement(TransformData transformData, float movementSpeed) {
        _destinationPosition = transformData.position.ToVector3;
        _destinationRotation = transformData.rotation.ToVector3;
        _movementSpeed = movementSpeed;
    }
}