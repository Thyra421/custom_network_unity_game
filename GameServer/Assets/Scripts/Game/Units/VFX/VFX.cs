using UnityEngine;

public class VFX : Unit
{
    private string _prefabName;
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

    public void Initialize(VFXsManager VFXsManager, string prefabName) {
        _VFXsManager = VFXsManager;
        _prefabName = prefabName;
    }

    public VFXData Data => new VFXData(Id, TransformData, _prefabName);
}