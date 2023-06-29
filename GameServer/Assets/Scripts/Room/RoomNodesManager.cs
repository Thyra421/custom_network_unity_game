using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomNodesManager : MonoBehaviour
{
    private readonly List<Node> _nodes = new List<Node>();
    private readonly List<Transform> _occupied = new List<Transform>();
    [SerializeField]
    private Room _room;

    private Transform FindFreeSpawn() {
        Transform randomTransform = GameManager.Current.RandomPlainSpawn;
        while (_occupied.Contains(randomTransform))
            randomTransform = GameManager.Current.RandomPlainSpawn;
        _occupied.Add(randomTransform);
        return randomTransform;
    }

    private Node CreateNode(DropSource dropSource) {
        Transform spawn = FindFreeSpawn();

        GameObject newObject = Instantiate(Resources.Load<GameObject>($"{SharedConfig.PREFABS_PATH}/{dropSource.Prefab.name}"), spawn.position, spawn.rotation, transform);
        Node newNode = newObject.AddComponent<Node>();
        newNode.Initialize(dropSource);
        _nodes.Add(newNode);
        return newNode;
    }

    private void PrepareBiome(DropSource[] dropSources, int amount) {
        for (int i = 0; i < amount; i++) {
            DropSource dropSource = dropSources[Random.Range(0, dropSources.Length)];
            CreateNode(dropSource);
        }
    }

    private IEnumerator Respawn(DropSource dropSource) {
        yield return new WaitForSeconds(dropSource.RespawnTimerInSeconds);
        Node newNode = CreateNode(dropSource);
        _room.PlayersManager.BroadcastTCP(new MessageSpawnNodes(new NodeData[] { newNode.Data }));
    }

    private void SpawnNodes() {
        PrepareBiome(Resources.LoadAll<DropSource>($"{SharedConfig.DROP_SOURCES_PATH}/Plains/Common"), 20);
        PrepareBiome(Resources.LoadAll<DropSource>($"{SharedConfig.DROP_SOURCES_PATH}/Plains/Rare"), 5);
    }

    private void Awake() {
        SpawnNodes();
    }

    public void RemoveNode(Node node) {
        _occupied.Remove(GameManager.Current.FindSpawn(node.transform.position));
        if (node.DropSource.RespawnTimerInSeconds != -1)
            StartCoroutine(Respawn(node.DropSource));
        _nodes.Remove(node);
        Destroy(node.gameObject);
    }

    public Node FindNode(string id) {
        return _nodes.Find((Node m) => m.Id == id);
    }

    public NodeData[] NodeDatas => _nodes.Select((Node node) => node.Data).ToArray();
    public List<Node> Nodes => _nodes;
}