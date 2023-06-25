using UnityEngine;

public class Movement : MonoBehaviour
{
    private TransformData _transformData;
    private AnimationData _animationData;
    private TransformData _lastTransform;
    private float _elapsedTime;

    private void Awake() {
        _transformData = new TransformData(transform);
    }

    private void Update() {
        _elapsedTime += Time.deltaTime;
    }

    public void SetMovement(TransformData transformData, AnimationData animation) {
        _transformData = transformData;
        transform.position = transformData.position.ToVector3;
        transform.eulerAngles = transformData.rotation.ToVector3;
        _animationData = animation;
        _elapsedTime = 0;
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

    public AnimationData AnimationData => _animationData;

    public bool IsMoving => _elapsedTime <= .1f;
}