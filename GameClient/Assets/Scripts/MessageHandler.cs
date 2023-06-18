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
    public delegate void OnMessagePlayerAttackHandler(MessagePlayerAttack messageAttack);
    public delegate void OnMessageDamageHandler(MessageDamage messageDamage);

    public OnMessageJoinedGameHandler onMessageJoinedGame;
    public OnMessageMovementsHandler onMessageMovements;
    public OnMessageLeftGameHandler onMessageLeftGame;
    public OnMessageGameStateHandler onMessageGameState;
    public OnMessagePlayerAttackHandler onMessagePlayerAttack;
    public OnMessageDamageHandler onMessageDamage;

    public static MessageHandler Current => _current;
}
