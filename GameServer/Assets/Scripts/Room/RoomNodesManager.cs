using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomNodesManager : MonoBehaviour
{
    private readonly List<Transform> _occupied = new List<Transform>();
    [SerializeField]
    private Room _room;

    public List<Node> Nodes { get; } = new List<Node>();

    private Transform FindFreeSpawn(NodeArea area) {
        Transform randomTransform = area.RandomSpawn;
        while (_occupied.Contains(randomTransform))
            randomTransform = area.RandomSpawn;
        _occupied.Add(randomTransform);
        return randomTransform;
    }

    private Node CreateNode(DropSource dropSource, NodeArea area) {
        Transform spawn = FindFreeSpawn(area);

        GameObject newObject = Instantiate(dropSource.Prefab, spawn.position, spawn.rotation, transform);
        Node newNode = newObject.AddComponent<Node>();
        newObject.name = $"{dropSource.name} {newNode.Id}";
        newNode.Initialize(dropSource, area);
        Nodes.Add(newNode);
        return newNode;
    }

    private void PrepareNodes() {
        foreach (NodeArea area in GameManager.Current.NodeAreas)
            foreach (NodeAreaEntry entry in area.Entries)
                for (int i = 0; i < entry.Amount; i++)
                    CreateNode(entry.DropSource, area);
    }

    private IEnumerator Respawn(DropSource dropSource, NodeArea area) {
        yield return new WaitForSeconds(dropSource.RespawnTimerInSeconds);
        Node newNode = CreateNode(dropSource, area);
        _room.PlayersManager.BroadcastTCP(new MessageSpawnNodes(new NodeData[] { newNode.Data }));
    }

    private void Awake() {
        PrepareNodes();
    }

    public void RemoveNode(Node node) {
        _room.PlayersManager.BroadcastTCP(new MessageDespawnObject(node.Id));
        _occupied.Remove(node.NodeArea.FindSpawn(node.transform.position));
        if (node.DropSource.RespawnTimerInSeconds != -1)
            StartCoroutine(Respawn(node.DropSource, node.NodeArea));
        Nodes.Remove(node);
        Destroy(node.gameObject);
    }

    public Node FindNode(string id) {
        return Nodes.Find((Node m) => m.Id == id);
    }

    public NodeData[] NodeDatas => Nodes.Select((Node node) => node.Data).ToArray();
}