using UnityEngine;

public class PlayerHealth
{
    private readonly Player _player;

    private int _maxHealth = 100;
    private int _currentHealth;

    public int CurrentHealth {
        get => _currentHealth;
        set {
            _currentHealth = Mathf.Clamp(value, 0, _maxHealth);
            _player.Room.PlayersManager.BroadcastTCP(new MessageHealthChanged(_player.Id, CurrentHealth, MaxHealth));
        }
    }
    public int MaxHealth {
        get => _maxHealth;
        set {
            _maxHealth = value;
            _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
            _player.Room.PlayersManager.BroadcastTCP(new MessageHealthChanged(_player.Id, CurrentHealth, MaxHealth));
        }
    }

    public PlayerHealth(Player player) {
        _player = player;
        _currentHealth = _maxHealth;
    }    
}
