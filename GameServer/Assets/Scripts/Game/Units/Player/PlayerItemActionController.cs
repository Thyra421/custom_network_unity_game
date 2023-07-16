using System.Linq;

public class PlayerItemActionController : IItemActionController
{
    private readonly Player _player;

    public PlayerItemActionController(Player player) {
        _player = player;
    }

    public void Use(UsableItem item) {
        if (_player.Inventory.Contains(item, 1) && !_player.Cooldowns.Any(item))
            foreach (ItemAction entry in item.Actions)
                typeof(IItemActionController).GetMethod(entry.MethodName).Invoke(this, entry.Parameters.Select((ActionParameter param) => param.ToObject).ToArray());
    }

    public void RestoreHealth(int amount) {
        _player.Statistics.CurrentHealth += amount;
        _player.Room.PlayersManager.BroadcastTCP(new MessageHealthChanged(_player.Id, _player.Statistics.CurrentHealth, _player.Statistics.MaxHealth));
    }
}