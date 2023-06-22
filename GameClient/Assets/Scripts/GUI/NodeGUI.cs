using UnityEngine;
using UnityEngine.UI;

public class NodeGUI : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    public Slider Slider => slider;

    public void OnChanged(int value) {
        Slider.value = value;
    }
}
