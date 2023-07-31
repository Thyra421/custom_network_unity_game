using System.Collections.Generic;
using UnityEngine;

public class NodesManager : MonoBehaviour
{
    private readonly List<Node> _nodes = new List<Node>();

    public static NodesManager Current { get; private set; }

    public delegate void OnAddedNodeHandler(Node node);
    public delegate void OnRemovedNodeHandler(Node node);
    private event OnAddedNodeHandler OnAddedNode;
    private event OnRemovedNodeHandler OnRemovedNode;

    private Node FindNode(string id) => _nodes.Find((Node n) => n.Id == id);

    private void CreateNode(NodeData data) {
        DropSource dropSource = Resources.Load<DropSource>($"{SharedConfig.Current.DropSourcesPath}/{data.dropSourceName}");
        GameObject newObject = Instantiate(dropSource.Prefab, data.transformData.position.ToVector3, Quaternion.Euler(data.transformData.rotation.ToVector3));
        Node newNode = newObject.AddComponent<Node>();
        newNode.Initialize(data.id);
        _nodes.Add(newNode);
        OnAddedNode?.Invoke(newNode);
    }

    private void RemoveNode(string id) {
        Node node = FindNode(id);
        OnRemovedNode?.Invoke(node);
        _nodes.Remove(node);
        Destroy(node.gameObject);
    }

    private void OnMessageSpawnNodes(MessageSpawnNodes messageSpawnNodes) {
        foreach (NodeData o in messageSpawnNodes.nodes)
            CreateNode(o);
    }

    private void OnMessageDespawnNode(MessageDespawnNode messageDespawnNode) {
        RemoveNode(messageDespawnNode.id);
    }

    private void Awake() {
        if (Current == null)
            Current = this;
        else
            Destroy(gameObject);

        MessageHandler.Current.OnMessageDespawnNodeEvent += OnMessageDespawnNode;
        MessageHandler.Current.OnMessageSpawnNodesEvent += OnMessageSpawnNodes;
    }
}