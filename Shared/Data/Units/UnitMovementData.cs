public struct UnitMovementData
{
    public string id;
    public TransformData transform;
    public float timestamp;

    public UnitMovementData(string id, TransformData transform, float timestamp) {
        this.id = id;
        this.transform = transform;
        this.timestamp = timestamp;
    }
}