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

    public override string ToString() => $"TransformData({position}, {rotation})";

    public bool Equals(TransformData other) {
        if (other == null)
            return false;
        return other.position.Equals(position) && other.rotation.Equals(rotation);
    }
}
