namespace TestUDP;

public class TransformData
{
    public Vector3Data position;
    public Vector3Data rotation;

    public TransformData() {
    }

    public TransformData(Vector3Data position, Vector3Data rotation) {
        this.position = position;
        this.rotation = rotation;
    }

    public static TransformData Zero() => new TransformData(Vector3Data.Zero(), Vector3Data.Zero());
}
