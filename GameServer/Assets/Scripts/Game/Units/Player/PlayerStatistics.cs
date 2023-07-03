using UnityEngine;

public class PlayerStatistics
{
    private int _maxHealth = 100;
    private int _currentHealth;

    public PlayerStatistics() {
        _currentHealth = _maxHealth;
    }

    public int CurrentHealth {
        get => _currentHealth;
        set => _currentHealth = Mathf.Clamp(value, 0, _maxHealth);
    }

    public int MaxHealth {
        get => _maxHealth;
        set {
            _maxHealth = value;
            _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
        }
    }
}
