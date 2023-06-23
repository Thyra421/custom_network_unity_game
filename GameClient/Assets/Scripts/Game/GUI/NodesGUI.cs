using System.Collections.Generic;
using UnityEngine;

public class NodesGUI : MonoBehaviour
{
    [SerializeField]
    private NodesManager _nodesManager;
    [SerializeField]
    private GameObject _nodeGUITemplate;
    private readonly List<NodeGUI> _nodeGUIs = new List<NodeGUI>();

    private NodeGUI Find(Node node) => _nodeGUIs.Find((NodeGUI n) => n.Node == node);

    private void OnAddedNode(Node node) {
        NodeGUI nodeGUI = Instantiate(_nodeGUITemplate, node.transform).GetComponent<NodeGUI>();
        nodeGUI.Initialize(node);
        _nodeGUIs.Add(nodeGUI);
    }

    private void OnRemovedNode(Node node) {
        NodeGUI nodeGUI = Find(node);
        _nodeGUIs.Remove(nodeGUI);
        Destroy(nodeGUI.gameObject);
    }

    private void Awake() {
        _nodesManager.OnAddedNodeEvent += OnAddedNode;
        _nodesManager.OnRemovedNodeEvent += OnRemovedNode;
    }
}