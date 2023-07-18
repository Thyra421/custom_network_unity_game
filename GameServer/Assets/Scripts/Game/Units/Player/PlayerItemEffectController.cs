using System.Linq;

public class PlayerItemEffectController : IItemEffectController
{
    private readonly Player _player;

    public PlayerItemEffectController(Player player) {
        _player = player;
    }

    public void Use(UsableItem item) {
        if (_player.Inventory.Contains(item, 1) && !_player.Cooldowns.Any(item))
            foreach (ItemEffect effect in item.Effects)
                typeof(PlayerItemEffectController).GetMethod(effect.MethodName).Invoke(this, effect.Parameters.Select((EffectParameter param) => param.ToObject).ToArray());
    }

    public void RestoreHealth(int amount) {
        _player.Statistics.CurrentHealth += amount;
        _player.Room.PlayersManager.BroadcastTCP(new MessageHealthChanged(_player.Id, _player.Statistics.CurrentHealth, _player.Statistics.MaxHealth));
    }
}