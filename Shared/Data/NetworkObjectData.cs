public struct NetworkObjectData
{
    public string id;
    public TransformData transform;
    public string prefabName;

    public NetworkObjectData(string id, TransformData transform, string prefabName) {
        this.id = id;
        this.transform = transform;
        this.prefabName = prefabName;
    }
}