using UnityEngine;

public class NPCHealth : CharacterHealth
{
    private NPC NPC => _character as NPC;

    public override int CurrentHealth {
        get => _currentHealth;
        set {
            _currentHealth = Mathf.Clamp(value, 0, _maxHealth);

            NPC.Room.PlayersManager.BroadcastTCP(new MessageHealthChanged(NPC.CharacterData, CurrentHealth, MaxHealth));
        }
    }
    public override int MaxHealth {
        get => _maxHealth;
        set {
            _maxHealth = value;
            _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
            NPC.Room.PlayersManager.BroadcastTCP(new MessageHealthChanged(NPC.CharacterData, CurrentHealth, MaxHealth));
        }
    }

    public NPCHealth(NPC NPC) : base(NPC) { }
}
