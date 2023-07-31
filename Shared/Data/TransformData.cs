using UnityEngine;

public struct TransformData
{
    public Vector3Data position;
    public Vector3Data rotation;

    public static TransformData Zero => new TransformData(Vector3Data.Zero, Vector3Data.Zero);

    public TransformData(Vector3Data position, Vector3Data rotation) {
        this.position = position;
        this.rotation = rotation;
    }

    public TransformData(Transform transform) {
        position = new Vector3Data(transform.position);
        rotation = new Vector3Data(transform.eulerAngles);
    }

    public readonly bool Equals(TransformData other) {
        return other.position.Equals(position) && other.rotation.Equals(rotation);
    }
}