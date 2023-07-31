using UnityEngine;

public class PlayerHealth : CharacterHealth
{
    private Player Player => _character as Player;

    public override int CurrentHealth {
        get => _currentHealth;
        set {
            _currentHealth = Mathf.Clamp(value, 0, _maxHealth);

            Player.Room.PlayersManager.BroadcastTCP(new MessageHealthChanged(Player.CharacterData, CurrentHealth, MaxHealth));
        }
    }
    public override int MaxHealth {
        get => _maxHealth;
        set {
            _maxHealth = value;
            _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
            Player.Room.PlayersManager.BroadcastTCP(new MessageHealthChanged(Player.CharacterData, CurrentHealth, MaxHealth));
        }
    }

    public PlayerHealth(Player character) : base(character) { }
}
