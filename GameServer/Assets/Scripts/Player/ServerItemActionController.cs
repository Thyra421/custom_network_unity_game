using System.Linq;

public class PlayerItemActionController : ItemActionController
{
    private readonly Player _player;

    public PlayerItemActionController(Player player) {
        _player = player;
    }

    public void Use(UsableItem item) {
        foreach (ItemAction entry in item.Actions) {
            typeof(ItemActionController).GetMethod(entry.MethodName).Invoke(this, entry.Parameters.Select((ActionParameter param) => param.ToObject).ToArray());
        }
    }

    public override void RestoreHealth(int amount) {
        _player.Statistics.CurrentHealth += amount;
        _player.Room.PlayersManager.BroadcastTCP(new MessageHealthChanged(_player.Id, _player.Statistics.CurrentHealth, _player.Statistics.MaxHealth));
    }
}