namespace TestUDP;

public class Vector3Data
{
    public float x;
    public float y;
    public float z;

    public Vector3Data() {
    }

    public Vector3Data(float x, float y, float z) {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public override string ToString() => $"Vector3Data({x}, {y}, {z})";

    public bool Equals(Vector3Data other) {
        if (other == null)
            return false;
        return other.x == x && other.y == y && other.z == z;
    }

    public static Vector3Data Zero() => new Vector3Data(0, 0, 0);
}
