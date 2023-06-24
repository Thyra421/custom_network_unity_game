using System.Collections.Generic;
using UnityEngine;

public class NodesManager : MonoBehaviour
{
    private readonly List<Node> _nodes = new List<Node>();
    private event OnAddedNodeHandler _onAddedNode;
    private event OnRemovedNodeHandler _onRemovedNode;

    private Node FindNode(string id) => _nodes.Find((Node n) => n.Id == id);

    private void CreateNode(NodeData data) {
        GameObject newObject = Instantiate(Resources.Load<GameObject>($"{SharedConfig.PREFABS_PATH}/{data.prefabName}"), data.transform.position.ToVector3, Quaternion.Euler(data.transform.rotation.ToVector3));
        Node newNode = newObject.AddComponent<Node>();
        newNode.Initialize(data.id, data.remainingLoots);
        _nodes.Add(newNode);
        _onAddedNode(newNode);
    }

    private void RemoveNode(string id) {
        Node node = FindNode(id);
        _onRemovedNode(node);
        _nodes.Remove(node);
        Destroy(node.gameObject);
    }

    private void OnMessageSpawnNodes(MessageSpawnNodes messageSpawnNodes) {
        foreach (NodeData o in messageSpawnNodes.nodes) {
            CreateNode(o);
        }
    }

    private void OnMessageLooted(MessageLooted messageLooted) {
        FindNode(messageLooted.id).RemoveLoot();
    }

    private void OnMessageDespawnObject(MessageDespawnObject messageDespawnObject) {
        RemoveNode(messageDespawnObject.id);
    }

    private void Awake() {
        MessageHandler.OnMessageDespawnObjectEvent += OnMessageDespawnObject;
        MessageHandler.OnMessageSpawnNodesEvent += OnMessageSpawnNodes;
        MessageHandler.OnMessageLootedEvent += OnMessageLooted;
    }

    public delegate void OnAddedNodeHandler(Node node);
    public delegate void OnRemovedNodeHandler(Node node);

    public event OnAddedNodeHandler OnAddedNodeEvent {
        add => _onAddedNode += value;
        remove => _onAddedNode -= value;
    }

    public event OnRemovedNodeHandler OnRemovedNodeEvent {
        add => _onRemovedNode += value;
        remove => _onRemovedNode -= value;
    }
}