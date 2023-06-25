using UnityEngine;

public class Movement : MonoBehaviour
{
    private TransformData _transformData;
    private AnimationData _animationData;
    private TransformData _lastTransform;

    private void Awake() {
        _transformData = new TransformData(transform);
    }

    public void SetMovement(TransformData transform, AnimationData animation) {
        TransformData = transform;
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

    public TransformData TransformData {
        get => _transformData;
        set {
            _transformData = value;
            transform.position = value.position.ToVector3;
            transform.eulerAngles = value.rotation.ToVector3;
        }
    }
    public AnimationData AnimationData {
        get => _animationData;
        set => _animationData = value;
    }    
}