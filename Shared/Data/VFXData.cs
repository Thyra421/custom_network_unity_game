public struct VFXData
{
    public string id;
    public TransformData transformData;
    public string prefabName;

    public VFXData(string id, TransformData transformData, string prefabName) {
        this.id = id;
        this.transformData = transformData;
        this.prefabName = prefabName;
    }
}

public struct VFXMovementData
{
    public string id;
    public TransformData transformData;
    public float speed;

    public VFXMovementData(string id, TransformData transformData, float speed) {
        this.id = id;
        this.transformData = transformData;
        this.speed = speed;
    }
}