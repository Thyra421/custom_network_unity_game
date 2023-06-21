using UnityEngine;

public class MessageHandler : MonoBehaviour
{
    private static MessageHandler _current;

    private void Awake() {
        if (_current == null) {
            _current = this;
        } else
            Destroy(gameObject);
    }

    public delegate void OnMessageJoinedGameHandler(MessageJoinedGame messageJoinedGame);
    public delegate void OnMessageMovementsHandler(MessageMovements messageMovements);
    public delegate void OnMessageLeftGameHandler(MessageLeftGame messageLeftGame);
    public delegate void OnMessageGameStateHandler(MessageGameState messageGameState);
    public delegate void OnMessageAttackedHandler(MessageAttacked messageAttacked);
    public delegate void OnMessageDamageHandler(MessageDamage messageDamage);
    public delegate void OnMessagePickedUpHandler(MessagePickedUp messagePickedUp);
    public delegate void OnMessageSpawnObjects(MessageSpawnObjects messageSpawnObjects);

    public OnMessageJoinedGameHandler onMessageJoinedGame;
    public OnMessageMovementsHandler onMessageMovements;
    public OnMessageLeftGameHandler onMessageLeftGame;
    public OnMessageGameStateHandler onMessageGameState;
    public OnMessageAttackedHandler onMessageAttacked;
    public OnMessageDamageHandler onMessageDamage;
    public OnMessagePickedUpHandler onMessagePickedUp;
    public OnMessageSpawnObjects onMessageSpawnObjects;

    public static MessageHandler Current => _current;
}
