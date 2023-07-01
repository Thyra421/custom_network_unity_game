using UnityEngine;

public class PlayersGUI : MonoBehaviour
{
    private static PlayersGUI _current;

    public static PlayersGUI Current => _current;

    private void OnAddedPlayer(Character player) {

    }

    private void OnRemovedPlayer(Character player) {

    }

    private void Awake() {
        if (_current == null)
            _current = this;
        else
            Destroy(gameObject);
        PlayersManager.Current.OnAddedPlayerEvent += OnAddedPlayer;
        PlayersManager.Current.OnRemovedPlayerEvent += OnRemovedPlayer;
    }
}