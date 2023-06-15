using UnityEngine;

public class ObjectData
{
    public string id;
    public TransformData transform;
}

public class TransformData
{
    public Vector3Data position;
    public Vector3Data rotation;

    public TransformData() {
    }

    public override string ToString() => position + " | " + rotation;

    public TransformData(Vector3Data position, Vector3Data rotation) {
        this.position = position;
        this.rotation = rotation;
    }

    public TransformData(Transform transform) {
        position = new Vector3Data(transform.position);
        rotation = new Vector3Data(transform.eulerAngles);
    }

    public bool Equals(TransformData other) {
        if (other == null)
            return false;
        return other.position.Equals(position) && other.rotation.Equals(rotation);
    }

    public static TransformData Zero() => new TransformData(Vector3Data.Zero(), Vector3Data.Zero());
}

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

    public override string ToString() => x + "," + y + "," + z;

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

    public bool Equals(Vector3Data other) {
        if (other == null)
            return false;
        return other.x == x && other.y == y && other.z == z;
    }
}