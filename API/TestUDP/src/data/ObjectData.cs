namespace TestUDP;

public class ObjectData
{
    public readonly string id;
    public Vector3Data position;

    public ObjectData() {
        id = id = Utils.GenerateUUID();
        position = Vector3Data.Zero();
    }
}