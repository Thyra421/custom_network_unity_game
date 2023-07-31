using UnityEngine;

public class CharactersManager : MonoBehaviour
{
    private void OnMessageHealthChanged(MessageHealthChanged messageHealthChanged) {
        switch (messageHealthChanged.character.type) {
            case CharacterType.Player:
                PlayersManager.Current.OnMessageHealthChanged(messageHealthChanged);
                break;
            case CharacterType.NPC:
                NPCsManager.Current.OnMessageHealthChanged(messageHealthChanged);
                break;
        }
    }

    private void Awake() {
        MessageHandler.Current.OnMessageHealthChangedEvent += OnMessageHealthChanged;
    }
}