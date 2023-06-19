using UnityEngine;

public class Mushroom : MonoBehaviour
{
    private readonly string _id = Utils.GenerateUUID();
    private TransformData _transformData;

    private void Awake() {
        _transformData = new TransformData(transform);
    }

    public ObjectData Data => new ObjectData(_id, _transformData, "Mushroom");

    public string Id => _id;
}