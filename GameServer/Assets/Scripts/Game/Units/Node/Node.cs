using System.Collections.Generic;
using UnityEngine;

public class Node : Unit
{
    private readonly Queue<RawMaterial> _loots = new Queue<RawMaterial>();
    private TransformData _transformData;

    public DropSource DropSource { get; private set; }
    public NodeArea NodeArea { get; private set; }

    private void Awake() {
        _transformData = new TransformData(transform);
    }

    public void Initialize(DropSource dropSource, NodeArea nodeArea) {
        DropSource = dropSource;
        NodeArea = nodeArea;
        int amount = Random.Range(DropSource.MinInclusive, DropSource.MaxExclusive);
        for (int i = 0; i < amount; i++)
            _loots.Enqueue(DropSource.RandomLoot as RawMaterial);
    }

    public void RemoveOne() => _loots.Dequeue();

    public NodeData Data => new NodeData(Id, _transformData, DropSource.name);

    public Item Loot => _loots.Peek();

    public int RemainingLoots => _loots.Count;
}