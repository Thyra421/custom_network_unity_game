using UnityEngine;

public class RemoteUnitMovement : MonoBehaviour
{
    private Vector3 _destinationPosition;
    private Vector3 _destinationRotation;
    private float _movementSpeed;
    protected float _distance;

    private void Move() {
        _distance = Vector3.Distance(transform.position, _destinationPosition);

        Debugger.Current.debugText.text = _distance.ToString();
        if (_distance <= _movementSpeed * Time.deltaTime || _distance > 10)
            transform.position = _destinationPosition;
        else
            transform.position = Vector3.Lerp(transform.position, _destinationPosition, _movementSpeed * Time.deltaTime);
    }

    private void Rotate() {
        transform.eulerAngles = _destinationRotation;
    }

    protected virtual void Awake() {
        _destinationPosition = transform.position;
        _destinationRotation = transform.eulerAngles;
    }

    protected virtual void Update() {
        Move();
        Rotate();
    }

    public void SetMovement(TransformData transformData, float movementSpeed) {
        _destinationPosition = transformData.position.ToVector3;
        _destinationRotation = transformData.rotation.ToVector3;
        _movementSpeed = movementSpeed;
    }
}