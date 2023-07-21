using System;
using UnityEngine;

[Serializable]
public class NodeAreaEntry
{
    [SerializeField]
    private int _amount;
    [SerializeField]
    private DropSource _dropSource;

    public int Amount => _amount;

    public DropSource DropSource => _dropSource;
}

[Serializable]
public class NodeArea
{
    [SerializeField]
    private Transform[] _spawns;
    [SerializeField]
    private NodeAreaEntry[] _entries;

    public Transform FindSpawn(Vector3 spawnPosition) => Array.Find(_spawns, (Transform t) => t.position == spawnPosition);

    public Transform RandomSpawn => _spawns[UnityEngine.Random.Range(0, _spawns.Length)];

    public NodeAreaEntry[] Entries => _entries;
}