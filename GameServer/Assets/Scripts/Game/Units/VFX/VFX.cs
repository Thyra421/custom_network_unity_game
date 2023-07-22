public class VFX : Unit
{
    private string _prefabName;
    private float _speed;
    private VFXsManager _VFXsManager;
    private TransformData _lastTransform;

    private TransformData TransformData => new TransformData(transform);

    private void OnDestroy() {
        _VFXsManager.RemoveVFX(this);
    }

    public bool UpdateTransformIfChanged() {
        if (_lastTransform.Equals(TransformData))
            return false;
        else {
            _lastTransform = TransformData;
            return true;
        }
    }

    public void Initialize(VFXsManager VFXsManager, string prefabName, float speed) {
        _VFXsManager = VFXsManager;
        _prefabName = prefabName;
        _speed = speed;
    }

    public VFXData Data => new VFXData(Id, TransformData, _prefabName);

    public VFXMovementData MovementData => new VFXMovementData(Id, TransformData, _speed);
}