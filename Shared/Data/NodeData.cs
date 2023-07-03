public struct NodeData
{
    public string id;
    public TransformData transformData;
    public string dropSourceName;

    public NodeData(string id, TransformData transformData, string dropSourceName) {
        this.id = id;
        this.transformData = transformData;
        this.dropSourceName = dropSourceName;
    }
}