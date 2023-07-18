using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private TransformData _lastTransform;
    private float _elapsedTime;

    public PlayerAnimationData AnimationData { get; private set; }

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
        AnimationData = animation;
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

    public bool IsMoving => _elapsedTime <= .05f;
}