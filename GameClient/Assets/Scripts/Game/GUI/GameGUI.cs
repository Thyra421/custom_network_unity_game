using UnityEngine;

[RequireComponent(typeof(PlayersGUI))]
[RequireComponent(typeof(InventoryGUI))]
[RequireComponent(typeof(CraftingGUI))]
public class GameGUI : MonoBehaviour
{
    [SerializeField]
    private GameManager _gameManager;
}