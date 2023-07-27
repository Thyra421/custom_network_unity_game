using UnityEngine;

public class PlayersGUIManager : MonoBehaviour
{
    public static PlayersGUIManager Current { get; private set; }

    private void OnAddedPlayer(Character player) { }

    private void OnRemovedPlayer(Character player) { }

    private void Awake() {
        if (Current == null)
            Current = this;
        else
            Destroy(gameObject);
        PlayersManager.Current.OnAddedPlayer += OnAddedPlayer;
        PlayersManager.Current.OnRemovedPlayer += OnRemovedPlayer;
    }
}