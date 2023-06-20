using UnityEngine;

public class Node : MonoBehaviour
{
    private readonly string _id = Utils.GenerateUUID();
    private TransformData _transformData;
    private RawMaterial _material;

    private void Awake() {
        _transformData = new TransformData(transform);
    }

    public ObjectData Data => new ObjectData(_id, _transformData, _material.Prefab.name);

    public string Id => _id;

    public RawMaterial Material {
        get => _material;
        set => _material = value;
    }
}