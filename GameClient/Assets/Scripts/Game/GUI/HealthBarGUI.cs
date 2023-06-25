using UnityEngine;
using UnityEngine.UI;

public class HealthBarGUI : MonoBehaviour
{
    [SerializeField]
    private Slider _slider;
    [SerializeField]
    private Player _player;

    private void OnChanged(int currentHealth, int maxHealth) {
        _slider.maxValue = maxHealth;
        _slider.value = currentHealth;
    }

    public void Awake() {
        _slider.maxValue = _player.Statistics.MaxHealth;
        _slider.value = _player.Statistics.CurrentHealth;
        _player.Statistics.OnChangedEvent += OnChanged;
    }

    public Player Player => _player;
}