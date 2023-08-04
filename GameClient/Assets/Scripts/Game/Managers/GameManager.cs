using UnityEngine;

[RequireComponent(typeof(PlayersManager))]
[RequireComponent(typeof(NodesManager))]
[RequireComponent(typeof(InventoryManager))]
public class GameManager : MonoBehaviour
{
    private void Start() {
        TCPClient.Send(new MessagePlay());
    }
}