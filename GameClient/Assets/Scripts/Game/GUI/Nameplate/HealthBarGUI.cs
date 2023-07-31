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
        _healthSlider.maxValue = _character.Health.MaxHealth;
        _healthSlider.value = _character.Health.CurrentHealth;
        _character.Health.OnChanged += OnChanged;
    }
}