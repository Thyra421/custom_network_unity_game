using UnityEngine;

public class OnUses
{
    private readonly Player _player;

    public OnUses(Player player) {
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
