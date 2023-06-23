using UnityEngine;

[RequireComponent(typeof(PlayersGUI))]
[RequireComponent(typeof(NodesGUI))]
[RequireComponent (typeof(InventoryGUI))]
public class GameGUI : MonoBehaviour
{
    [SerializeField]
    private GameManager _gameManager;    
}