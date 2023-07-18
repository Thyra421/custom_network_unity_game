using UnityEngine;

[RequireComponent(typeof(PlayersManager))]
[RequireComponent(typeof(NodesManager))]
[RequireComponent(typeof(InventoryManager))]
public class GameManager : MonoBehaviour
{
    public static GameManager Current { get; private set; }

    private void Awake() {
        if (Current == null)
            Current = this;
        else
            Destroy(gameObject);
    }

    private void Start() {
        TCPClient.Send(new MessagePlay());
    }
}