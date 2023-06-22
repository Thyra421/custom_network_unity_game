public struct NodeData
{
    public string id;
    public TransformData transform;
    public string assetName;
    public int remainingLoots;

    public NodeData(string id, TransformData transform, string assetName, int remainingLoots) {
        this.id = id;
        this.transform = transform;
        this.assetName = assetName;
        this.remainingLoots = remainingLoots;
    }
}