using UnityEngine;

public class PlayersGUI : MonoBehaviour
{
    [SerializeField]
    private PlayersManager _playersManager;

    private void OnAddedPlayer(Player player) {

    }

    private void OnRemovedPlayer(Player player) {

    }

    private void Awake() {
        _playersManager.OnAddedPlayerEvent += OnAddedPlayer;
        _playersManager.OnRemovedPlayerEvent += OnRemovedPlayer;
    }
}