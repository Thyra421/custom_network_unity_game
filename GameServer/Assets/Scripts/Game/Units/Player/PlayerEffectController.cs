using System.Linq;

public class PlayerEffectController : IEffectController
{
    private readonly Player _player;

    public PlayerEffectController(Player player) {
        _player = player;
    }

    public void Use(IUsable usable) {
        foreach (Effect effect in usable.Effects)
            typeof(PlayerEffectController).GetMethod(effect.MethodName).Invoke(this, effect.Parameters.Select((EffectParameter param) => param.ToObject).ToArray());
    }

    public void RestoreHealth(int amount) {
        _player.Statistics.CurrentHealth += amount;
        _player.Room.PlayersManager.BroadcastTCP(new MessageHealthChanged(_player.Id, _player.Statistics.CurrentHealth, _player.Statistics.MaxHealth));
    }

    public void DealDamage(int amount) {
        _player.Statistics.CurrentHealth -= amount;
        _player.Room.PlayersManager.BroadcastTCP(new MessageHealthChanged(_player.Id, _player.Statistics.CurrentHealth, _player.Statistics.MaxHealth));
    }

    public void ApplyAura(string test) {
        throw new System.NotImplementedException();
    }
}