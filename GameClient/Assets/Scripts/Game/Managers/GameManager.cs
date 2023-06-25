using UnityEngine;

[RequireComponent(typeof(PlayersManager))]
[RequireComponent(typeof(NodesManager))]
[RequireComponent(typeof(InventoryManager))]
public class GameManager : MonoBehaviour
{
    private static GameManager _current;

    private void Awake() {
        if (_current == null)
            _current = this;
        else
            Destroy(gameObject);
    }

    private void Start() {
        TCPClient.Send(new MessagePlay());
    }

    public static GameManager Current => _current;
}