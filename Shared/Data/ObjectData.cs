public struct NodeData
{
    public string id;
    public TransformData transform;
    public string prefabName;
    public int remainingLoots;

    public NodeData(string id, TransformData transform, string prefabName, int remainingLoots) {
        this.id = id;
        this.transform = transform;
        this.prefabName = prefabName;
        this.remainingLoots = remainingLoots;
    }
}