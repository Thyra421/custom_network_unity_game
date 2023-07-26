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