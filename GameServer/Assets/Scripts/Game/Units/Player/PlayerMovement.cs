using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerAnimationData _animationData;
    private TransformData _lastTransform;
    private float _elapsedTime;

    private void Update() {
        _elapsedTime += Time.deltaTime;
    }

    private void Awake() {
        _lastTransform = TransformData.Zero;
    }

    public void SetMovement(TransformData transformData, PlayerAnimationData animation) {
        if (!transformData.position.Equals(TransformData.position))
            _elapsedTime = 0;
        transform.position = transformData.position.ToVector3;
        transform.eulerAngles = transformData.rotation.ToVector3;
        _animationData = animation;
    }

    public bool UpdateTransformIfChanged() {
        if (_lastTransform.Equals(TransformData))
            return false;
        else {
            _lastTransform = TransformData;
            return true;
        }
    }

    public TransformData TransformData => new TransformData(transform);

    public PlayerAnimationData PlayerAnimationData => _animationData;

    public bool IsMoving => _elapsedTime <= .05f;
}