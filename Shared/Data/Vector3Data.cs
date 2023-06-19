using UnityEngine;

public struct Vector3Data
{
    public float x;
    public float y;
    public float z;

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

    public static Vector3Data Zero => new Vector3Data(0, 0, 0);

    public readonly Vector3 ToVector3 => new Vector3(x, y, z);

    public readonly bool Equals(Vector3Data other) {
        return other.x == x && other.y == y && other.z == z;
    }
}