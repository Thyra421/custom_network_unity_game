using UnityEngine;

public class Node : MonoBehaviour
{
    private readonly string _id = Utils.GenerateUUID();
    private TransformData _transformData;    
    private DropSource _dropSource;

    private void Awake() {
        _transformData = new TransformData(transform);
    }

    public ObjectData Data => new ObjectData(_id, _transformData, _dropSource.Prefab.name);

    public string Id => _id;

    public DropSource DropSource {
        get => _dropSource;
        set => _dropSource = value;
    }
}