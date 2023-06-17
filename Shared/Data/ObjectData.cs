public class ObjectData
{
    public string id;
    public TransformData transform;
    public MovementData movement;

    public ObjectData() {
        id = id = Utils.GenerateUUID();
        transform = TransformData.Zero();
        movement = MovementData.Zero();
    }
}