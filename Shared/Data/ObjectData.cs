public struct NodeData
{
    public string id;
    public TransformData transform;
    public string prefabName;

    public NodeData(string id, TransformData transform, string prefabName) {
        this.id = id;
        this.transform = transform;
        this.prefabName = prefabName;
    }
}