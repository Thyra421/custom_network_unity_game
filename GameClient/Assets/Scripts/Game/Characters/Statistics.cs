public class Statistics
{
    private int _currentHealth;
    private int _maxHealth = 100;

    public delegate void OnChangedHandler(int currentHealth, int maxHealth);
    public event OnChangedHandler OnChanged;

    public Statistics() {
        _currentHealth = _maxHealth;
    }

    public int CurrentHealth {
        get => _currentHealth;
        set {
            _currentHealth = value;
            OnChanged?.Invoke(_currentHealth, _maxHealth);
        }
    }

    public int MaxHealth {
        get => _maxHealth;
        set {
            _maxHealth = value;
            OnChanged?.Invoke(_currentHealth, _maxHealth);
        }
    }
}