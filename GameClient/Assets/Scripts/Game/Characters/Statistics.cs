using UnityEngine;

public class Statistics : MonoBehaviour
{
    protected int _currentHealth;
    protected int _maxHealth = 100;
    protected event OnChangedHandler _onChanged;

    private void Awake() {
        _currentHealth = _maxHealth;
    }

    public delegate void OnChangedHandler(int currentHealth, int maxHealth);

    public int CurrentHealth {
        get => _currentHealth;
        set {
            _currentHealth = value;
            _onChanged?.Invoke(_currentHealth, _maxHealth);
        }
    }

    public int MaxHealth {
        get => _maxHealth;
        set {
            _maxHealth = value;
            _onChanged?.Invoke(_currentHealth, _maxHealth);
        }
    }

    public event OnChangedHandler OnChangedEvent {
        add => _onChanged += value;
        remove => _onChanged -= value;
    }
}