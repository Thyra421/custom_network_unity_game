using UnityEngine;

public class ItemActionController
{
    private readonly Player _player;

    public ItemActionController(Player player) {
        _player = player;
    }

    public void RestoreHealth(int amount) {
        Debug.Assert(amount > 0, "Amount must be greater than 0");
        Debug.Log($"Healed {_player.gameObject.name} for {amount}");
    }

    public void StringMethod(string str) {

    }

    public void MultiparamMethod(int i, int str, bool b) {

    }


    public void ItemMethod(Item item) {

    }
}
