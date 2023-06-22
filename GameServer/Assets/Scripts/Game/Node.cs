using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    private readonly string _id = Utils.GenerateUUID();
    private readonly Queue<RawMaterial> _loots = new Queue<RawMaterial>();
    private TransformData _transformData;
    private DropSource _dropSource;

    private void Awake() {
        _transformData = new TransformData(transform);
    }

    public void GenerateDrops(DropSource dropSource) {
        _dropSource = dropSource;
        int amount = Random.Range(_dropSource.MinInclusive, _dropSource.MaxExclusive);
        for (int i = 0; i < amount; i++)
            _loots.Enqueue(_dropSource.RandomLoot as RawMaterial);
    }

    public void RemoveOne() => _loots.Dequeue();

    public NodeData Data => new NodeData(_id, _transformData, _dropSource.Prefab.name, _loots.Count);

    public string Id => _id;

    public Item Loot => _loots.Peek();

    public int RemainingLoots => _loots.Count;
}