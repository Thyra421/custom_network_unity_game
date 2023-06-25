using UnityEngine;
using UnityEngine.UI;

public class NameplateGUI : MonoBehaviour
{    
    [SerializeField]
    private Slider _slider;
    private Player _player;

    private void OnChanged(int currentHealth, int maxHealth) {
        _slider.maxValue = maxHealth;
        _slider.value = currentHealth;
    }

    public void Initialize(Player player) {
        _slider.maxValue = player.Statistics.MaxHealth;
        _slider.value = player.Statistics.CurrentHealth;
        _player = player;
        player.Statistics.OnChangedEvent += OnChanged;
    }

    public Player Player => _player;
}
