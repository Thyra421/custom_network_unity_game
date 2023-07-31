using UnityEngine;

public class Unit : MonoBehaviour
{
    private TransformData _lastTransform;

    public string Id { get; } = Utils.GenerateUUID();

    public TransformData TransformData => new TransformData(transform);

    public bool UpdateTransformIfChanged() {
        TransformData transformData = TransformData;

        if (_lastTransform.Equals(transformData))
            return false;
        else {
            _lastTransform = transformData;
            return true;
        }
    }
}