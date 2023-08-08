using UnityEngine;

public class RemoteUnitMovement : MonoBehaviour
{
    private Vector3 _destinationPosition;
    private Vector3 _destinationRotation;
    protected float _elapsedTime;
    private float _movementSpeed;
    private float _estimatedAngularSpeed;
    protected bool _isMoving;

    private void Move() {
        float distance = Vector3.Distance(transform.position, _destinationPosition);
        Debugger.Current.debugText.text = distance.ToString();

        // snap position for the final push
        if (distance <= (_movementSpeed * Time.deltaTime)) {
            _isMoving = false;
            transform.position = _destinationPosition;
        }
        // lerp to new position
        else {
            _isMoving = true;

            transform.position = Vector3.Lerp(transform.position, _destinationPosition, _movementSpeed * Time.deltaTime);
        }
    }

    private void Rotate() {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(_destinationRotation), _estimatedAngularSpeed * Time.deltaTime);
    }

    protected virtual void Awake() {
        _destinationPosition = transform.position;
        _destinationRotation = transform.eulerAngles;
    }

    protected virtual void Update() {
        Move();
        Rotate();
        _elapsedTime += Time.deltaTime;
    }

    public void SetMovement(TransformData transformData, float movementSpeed) {
        _destinationPosition = transformData.position.ToVector3;
        _destinationRotation = transformData.rotation.ToVector3;

        _movementSpeed = movementSpeed;
        _estimatedAngularSpeed = Quaternion.Angle(transform.rotation, Quaternion.Euler(_destinationRotation)) / _elapsedTime;

        _elapsedTime = 0;
    }
}