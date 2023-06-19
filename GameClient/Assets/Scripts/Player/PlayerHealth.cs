using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private int _health = 100;
    [SerializeField]
    private Slider _slider;

    public void TakeDamage(int amount) {
        _health -= amount;
        _slider.value = _health;
    }

    private void Awake() {
        _slider.maxValue = _health;
        _slider.value = _health;
    }
}
