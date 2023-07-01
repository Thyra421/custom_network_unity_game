using UnityEngine;
using UnityEngine.UI;

public class HealthBarGUI : MonoBehaviour
{
    [SerializeField]
    private Slider _healthSlider;
    [SerializeField]
    private Character _character;

    private void OnChanged(int currentHealth, int maxHealth) {
        _healthSlider.maxValue = maxHealth;
        _healthSlider.value = currentHealth;
    }

    public void Awake() {
        _healthSlider.maxValue = _character.Statistics.MaxHealth;
        _healthSlider.value = _character.Statistics.CurrentHealth;
        _character.Statistics.OnChangedEvent += OnChanged;
    }

    public Character Character => _character;
}