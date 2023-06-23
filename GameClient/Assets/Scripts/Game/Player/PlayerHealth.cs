using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private int _health = 100;

    private event OnChangedHandler _onChanged;

    public delegate void OnChangedHandler(int health);

    public void TakeDamage(int amount) {
        _health -= amount;
        _onChanged(_health);
    }

    public int Health => _health;

    public event OnChangedHandler OnChangedEvent {
        add => _onChanged += value;
        remove => _onChanged -= value;
    }
}