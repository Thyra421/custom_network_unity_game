using UnityEngine;
using UnityEngine.UI;

public class NameplateGUI : MonoBehaviour
{    
    [SerializeField]
    private Slider _slider;
    private Player _player;

    private void OnChanged(int amount) {
        _slider.value = amount;
    }

    public void Initialize(Player player) {
        _slider.maxValue = player.Health.Health;
        _slider.value = player.Health.Health;
        _player = player;
        player.Health.OnChangedEvent += OnChanged;
    }

    public Player Player => _player;
}
