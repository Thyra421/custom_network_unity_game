public struct ObjectData
{
    public string id;
    public TransformData transform;

    public ObjectData(string id, TransformData transform) {
        this.id = id;
        this.transform = transform;
    }
}