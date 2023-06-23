using UnityEngine;
using UnityEngine.UI;

public class NodeGUI : MonoBehaviour
{
    [SerializeField]
    private Slider _slider;
    private Node _node;

    public void Initialize(Node node) {
        _slider.maxValue = node.RemainingLoots;
        _slider.value = node.RemainingLoots;
        _node = node;
        node.OnChangedEvent += OnChanged;
    }

    public void OnChanged(int value) {
        _slider.value = value;
    }

    public Node Node => _node;
}
