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
        x = vector3.x;
        y = vector3.y;
        z = vector3.z;
    }

    public static Vector3Data Zero() {

        return new Vector3Data(0, 0, 0);
    }

    public Vector3 ToVector3() {

        return new Vector3(x, y, z);
    }
}
