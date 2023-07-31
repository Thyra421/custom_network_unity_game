using System;
using UnityEngine;

[Serializable]
public class NodeArea
{
    [SerializeField]
    private Transform[] _spawns;
    [SerializeField]
    private NodeAreaEntry[] _entries;

    public Transform RandomSpawn => _spawns[UnityEngine.Random.Range(0, _spawns.Length)];
    public NodeAreaEntry[] Entries => _entries;

    public Transform FindSpawn(Vector3 spawnPosition) => Array.Find(_spawns, (Transform t) => t.position == spawnPosition);
}