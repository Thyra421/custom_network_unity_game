using System.Collections.Generic;
using UnityEngine;

public class Node : Unit
{
    private readonly Queue<RawMaterial> _loots = new Queue<RawMaterial>();
    private DropSource _dropSource;
    private NodeArea _nodeArea;
    private TransformData _transformData;

    private void Awake() {
        _transformData = new TransformData(transform);
    }

    public void Initialize(DropSource dropSource, NodeArea nodeArea) {
        _dropSource = dropSource;
        _nodeArea = nodeArea;
        int amount = Random.Range(_dropSource.MinInclusive, _dropSource.MaxExclusive);
        for (int i = 0; i < amount; i++)
            _loots.Enqueue(_dropSource.RandomLoot as RawMaterial);
    }

    public void RemoveOne() => _loots.Dequeue();

    public NodeData Data => new NodeData(_id, _transformData, _dropSource.name);

    public Item Loot => _loots.Peek();

    public int RemainingLoots => _loots.Count;

    public DropSource DropSource => _dropSource;

    public NodeArea NodeArea => _nodeArea;
}