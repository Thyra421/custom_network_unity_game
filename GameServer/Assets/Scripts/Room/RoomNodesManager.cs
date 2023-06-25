using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomNodesManager : MonoBehaviour
{
    private readonly List<Node> _nodes = new List<Node>();

    private void PrepareBiome(DropSource[] dropSources, int amount, List<Transform> occupied) {
        for (int i = 0; i < amount; i++) {
            Transform randomTransform = GameManager.Current.RandomPlainSpawn;
            while (occupied.Contains(randomTransform))
                randomTransform = GameManager.Current.RandomPlainSpawn;
            occupied.Add(randomTransform);

            DropSource dropSource = dropSources[Random.Range(0, dropSources.Length)];

            GameObject newObject = Instantiate(Resources.Load<GameObject>($"{SharedConfig.PREFABS_PATH}/{dropSource.Prefab.name}"), randomTransform.position, randomTransform.rotation, transform);
            Node newNode = newObject.AddComponent<Node>();
            newNode.GenerateDrops(dropSource);
            _nodes.Add(newNode);
        }
    }

    private void SpawnNodes() {
        List<Transform> occupied = new List<Transform>();

        PrepareBiome(Resources.LoadAll<DropSource>($"{SharedConfig.DROP_SOURCES_PATH}/Plains/Common"), 20, occupied);
        PrepareBiome(Resources.LoadAll<DropSource>($"{SharedConfig.DROP_SOURCES_PATH}/Plains/Rare"), 5, occupied);
    }

    private void Awake() {
        SpawnNodes();
    }

    public void RemoveNode(Node node) {
        _nodes.Remove(node);
        Destroy(node.gameObject);
    }

    public Node FindNode(string id) {
        return _nodes.Find((Node m) => m.Id == id);
    }

    public NodeData[] NodeDatas => _nodes.Select((Node node) => node.Data).ToArray();
    public List<Node> Nodes => _nodes;
}