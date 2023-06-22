using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class ItemStack
{
    private readonly Item _item;
    private int _amount;

    public ItemStack(Item item, int amount) {
        _item = item;
        _amount = amount;
    }

    public void Remove(int amount) {
        _amount -= amount;
    }

    public void Add(int amount) {
        _amount += amount;
    }

    public Item Item => _item;

    public int Amount => _amount;
}

public class Inventory
{
    private Player _player;

    public Inventory(Player player) {
        _player = player;
    }

    private readonly List<ItemStack> _stacks = new List<ItemStack>();

    private bool Any(Item item) => _stacks.Any((ItemStack i) => i.Item == item);

    private ItemStack Find(Item item) => _stacks.Find((ItemStack i) => i.Item == item);

    public async Task<bool> Add(Item item, int amount) {
        // inventory full?
        if (_stacks.Count >= SharedConfig.INVENTORY_SPACE) {
            await _player.Client.Tcp.Send(new MessageError(MessageErrorType.inventoryFull));
            return false;
        }
        // already has unique item?
        else if (item.Property == ItemProperty.Unique && Any(item)) {
            await _player.Client.Tcp.Send(new MessageError(MessageErrorType.uniqueItem));
            return false;
        }
        // is stackable and already has a stack?
        else if (item.Property == ItemProperty.Stackable && Any(item)) {
            ItemStack stack = Find(item);
            stack.Add(amount);
            await _player.Client.Tcp.Send(new MessageInventoryAdd(item.name, amount));
            return true;
        }
        // is stackable and doesn't have any?
        else if (item.Property == ItemProperty.Stackable) {
            _stacks.Add(new ItemStack(item, amount));
            await _player.Client.Tcp.Send(new MessageInventoryAdd(item.name, amount));
            return true;
        }
        // is not stackable?
        else {
            _stacks.Add(new ItemStack(item, 1));
            await _player.Client.Tcp.Send(new MessageInventoryAdd(item.name, 1));

            if (amount > 1)
                return await Add(item, amount - 1);
            return true;
        }
    }

    public async void Remove(Item item, int amount) {
        ItemStack stack = Find(item);

        if (stack != null && stack.Amount >= amount) {
            stack.Remove(amount);
            if (stack.Amount <= 0)
                _stacks.Remove(stack);
            await _player.Client.Tcp.Send(new MessageInventoryRemove(item.name, amount));
        }
    }
}