using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RemoteUnitMovement : MonoBehaviour
{
    private Vector3 _destinationPosition;
    private Vector3 _destinationRotation;
    protected float _elapsedTime;
    private float _estimatedSpeed;
    private float _lastTimestamp;

    private void Move() {
        transform.position = Vector3.Lerp(transform.position, _destinationPosition, _estimatedSpeed * Time.deltaTime);
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
        _elapsedTime += Time.deltaTime;
    }

    public void SetMovement(TransformData transformData, float timestamp) {
        _destinationPosition = transformData.position.ToVector3;
        _destinationRotation = transformData.rotation.ToVector3;

        if (_lastTimestamp != 0) {
            float distance = Vector3.Distance(transform.position, _destinationPosition);
            float duration = timestamp - _lastTimestamp;
            _estimatedSpeed = distance / duration;
            Debugger.Current.debugText.text = $"{duration}";
        }
        _lastTimestamp = timestamp;

        _elapsedTime = 0;
    }
}