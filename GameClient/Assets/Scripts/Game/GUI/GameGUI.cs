using UnityEngine;

[RequireComponent(typeof(PlayersGUI))]
[RequireComponent(typeof(InventoryGUI))]
[RequireComponent(typeof(CraftingGUI))]
public class GameGUI : MonoBehaviour
{
    private static GameGUI _current;

    private void Awake() {
        if (_current == null)
            _current = this;
        else
            Destroy(gameObject);
    }

    public static GameGUI Current => _current;
}