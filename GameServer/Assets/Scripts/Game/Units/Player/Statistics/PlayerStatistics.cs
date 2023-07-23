using UnityEngine;

public struct StatisticsData
{
    public float _movementSpeed;
}

public class PlayerStatistics
{
    private readonly Player _player;
    private int _maxHealth = 100;
    private int _currentHealth;
    private StatisticsData _baseStatistics;

    public PlayerStatistics(Player player) {
        _player = player;
        _currentHealth = _maxHealth;
    }

    public int CurrentHealth {
        get => _currentHealth;
        set {
            _currentHealth = Mathf.Clamp(value, 0, _maxHealth);
            _player.Room.PlayersManager.BroadcastTCP(new MessageHealthChanged(_player.Id, _player.Statistics.CurrentHealth, _player.Statistics.MaxHealth));
        }
    }

    public int MaxHealth {
        get => _maxHealth;
        set {
            _maxHealth = value;
            _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
            _player.Room.PlayersManager.BroadcastTCP(new MessageHealthChanged(_player.Id, _player.Statistics.CurrentHealth, _player.Statistics.MaxHealth));
        }
    }

    public StatisticsData BaseStatistics => _baseStatistics;

    public StatisticsData Statistics {
        get {
            PlayerStatusEffectController statusEffectController = new PlayerStatusEffectController(_baseStatistics);
            return statusEffectController.Apply(_player.Alterations.Alterations);
        }
    }
}
