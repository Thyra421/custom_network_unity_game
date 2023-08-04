public struct UnitMovementData
{
    public string id;
    public TransformData transform;
    public float movementSpeed;

    public UnitMovementData(string id, TransformData transform, float movementSpeed) {
        this.id = id;
        this.transform = transform;
        this.movementSpeed = movementSpeed;
    }
}