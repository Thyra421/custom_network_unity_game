using UnityEngine;

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

    public Vector3Data(Vector3 vector3) {
        x = Mathf.Round(vector3.x * 1000) / 1000;
        y = Mathf.Round(vector3.y * 1000) / 1000;
        z = Mathf.Round(vector3.z * 1000) / 1000;
    }

    public static Vector3Data Zero() {
        return new Vector3Data(0, 0, 0);
    }

    public Vector3 ToVector3() {
        return new Vector3(x, y, z);
    }

    public override string ToString() => $"Vector3Data({x}, {y}, {z})";

    public bool Equals(Vector3Data other) {
        if (other == null)
            return false;
        return other.x == x && other.y == y && other.z == z;
    }
}