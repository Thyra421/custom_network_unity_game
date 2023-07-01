using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private TransformData _transformData;
    private PlayerAnimationData _animationData;
    private TransformData _lastTransform;
    private float _elapsedTime;

    private void Awake() {
        _transformData = new TransformData(transform);
    }

    private void Update() {
        _elapsedTime += Time.deltaTime;
    }

    public void SetMovement(TransformData transformData, PlayerAnimationData animation) {
        if (!transformData.position.Equals(_transformData.position))
            _elapsedTime = 0;
        _transformData = transformData;
        transform.position = transformData.position.ToVector3;
        transform.eulerAngles = transformData.rotation.ToVector3;
        _animationData = animation;
    }

    public bool UpdateTransformIfChanged() {
        if (_lastTransform.Equals(_transformData))
            return false;
        else {
            _lastTransform = _transformData;
            return true;
        }
    }

    public TransformData TransformData => _transformData;

    public PlayerAnimationData PlayerAnimationData => _animationData;

    public bool IsMoving => _elapsedTime <= .05f;
}