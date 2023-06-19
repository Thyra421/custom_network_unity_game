public struct ObjectData
{
    public string id;
    public TransformData transform;
    public string assetName;

    public ObjectData(string id, TransformData transform, string assetName) {
        this.id = id;
        this.transform = transform;
        this.assetName = assetName;
    }
}