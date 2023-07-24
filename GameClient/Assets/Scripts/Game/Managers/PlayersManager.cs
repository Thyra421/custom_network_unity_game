using System.Collections.Generic;
using UnityEngine;

public class PlayersManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _playerPrefab;
    [SerializeField]
    private LocalPlayer _myPlayer;
    private readonly List<RemotePlayer> _remotePlayers = new List<RemotePlayer>();

    public static PlayersManager Current { get; private set; }

    public delegate void OnAddedPlayerHandler(Character player);
    public delegate void OnRemovedPlayerHandler(Character player);
    public event OnAddedPlayerHandler OnAddedPlayer;
    public event OnRemovedPlayerHandler OnRemovedPlayer;

    private RemotePlayer FindPlayer(string id) => _remotePlayers.Find((RemotePlayer p) => p.Id == id);

    private void CreatePlayer(PlayerData data) {
        RemotePlayer newRemotePlayer = Instantiate(_playerPrefab, data.transformData.position.ToVector3, Quaternion.identity).GetComponent<RemotePlayer>();
        newRemotePlayer.Initialize(data.id);
        _remotePlayers.Add(newRemotePlayer);
        OnAddedPlayer?.Invoke(newRemotePlayer);
    }

    private void RemovePlayer(RemotePlayer remotePlayer) {
        OnRemovedPlayer?.Invoke(remotePlayer);
        _remotePlayers.Remove(remotePlayer);
        Destroy(remotePlayer.gameObject);
    }

    private void OnMessageGameState(MessageGameState messageGameState) {
        _myPlayer.Initialize(messageGameState.id);
        foreach (PlayerData p in messageGameState.players) {
            if (p.id != _myPlayer.Id)
                CreatePlayer(p);
        }
    }

    private void OnMessageJoinedGame(MessageJoinedGame messageJoinedGame) {
        if (messageJoinedGame.player.id != _myPlayer.Id)
            CreatePlayer(messageJoinedGame.player);
    }

    private void OnMessagePlayerMoved(MessagePlayerMoved serverMessagePlayerMoved) {
        foreach (PlayerData p in serverMessagePlayerMoved.players) {
            if (p.id == _myPlayer.Id)
                continue;

            RemotePlayer remotePlayer = FindPlayer(p.id);
            if (remotePlayer != null) {
                remotePlayer.Movement.DestinationPosition = p.transformData.position.ToVector3;
                remotePlayer.Movement.DestinationRotation = p.transformData.rotation.ToVector3;
                remotePlayer.Movement.PlayerAnimationData = p.animationData;
            }
        }
    }

    private void OnMessageLeftGame(MessageLeftGame serverMessageLeftGame) {
        if (serverMessageLeftGame.id == _myPlayer.Id)
            return;

        RemotePlayer remotePlayer = FindPlayer(serverMessageLeftGame.id);
        if (remotePlayer != null)
            RemovePlayer(remotePlayer);
    }

    private void OnMessageHealthChanged(MessageHealthChanged messageHealthChanged) {
        if (messageHealthChanged.id == _myPlayer.Id) {
            _myPlayer.Statistics.MaxHealth = messageHealthChanged.maxHealth;
            _myPlayer.Statistics.CurrentHealth = messageHealthChanged.currentHealth;
        } else {
            RemotePlayer remotePlayer = FindPlayer(messageHealthChanged.id);
            if (remotePlayer != null) {
                remotePlayer.Statistics.MaxHealth = messageHealthChanged.maxHealth;
                remotePlayer.Statistics.CurrentHealth = messageHealthChanged.currentHealth;
            }
        }
    }

    private void OnMessageChannel(MessageChannel messageChannel) {
        if (messageChannel.id == _myPlayer.Id)
            _myPlayer.Channel(messageChannel.activityName, messageChannel.ticks, messageChannel.intervalTimeInSeconds);
        else
            FindPlayer(messageChannel.id)?.Channel(messageChannel.activityName, messageChannel.ticks, messageChannel.intervalTimeInSeconds);
    }

    private void OnMessageCast(MessageCast messageCast) {
        if (messageCast.id == _myPlayer.Id)
            _myPlayer.Cast(messageCast.activityName, messageCast.castTimeInSeconds);
        else
            FindPlayer(messageCast.id)?.Cast(messageCast.activityName, messageCast.castTimeInSeconds);
    }

    private void OnMessageStopActivity(MessageStopActivity messageStopActivity) {
        if (messageStopActivity.id == _myPlayer.Id)
            _myPlayer.StopActivity();
        else
            FindPlayer(messageStopActivity.id)?.StopActivity();
    }

    private void OnMessageEquiped(MessageEquiped messageEquiped) {
        Weapon weapon = Resources.Load<Weapon>($"{SharedConfig.ITEMS_PATH}/{messageEquiped.weaponName}");

        if (messageEquiped.id == _myPlayer.Id) {
            AbilitiesManager.Current.Equip(weapon);
            // TODO equip weapon skin
        } else
            // TODO equip weapon skin
            ;
    }

    private void OnMessageTriggerAnimation(MessageTriggerAnimation messageTriggerAnimation) {
        if (messageTriggerAnimation.id == _myPlayer.Id)
            _myPlayer.TriggerAnimation(messageTriggerAnimation.animationName);
        else
            FindPlayer(messageTriggerAnimation.id)?.TriggerAnimation(messageTriggerAnimation.animationName);
    }

    private void OnMessageAddAlteration(MessageAddAlteration messageAddAlteration) {
        Alteration alteration = Resources.Load<Alteration>($"{SharedConfig.ALTERATIONS_PATH}/{messageAddAlteration.alteration.alterationName}");
        AlterationController alterationController = new AlterationController(alteration, messageAddAlteration.alteration.remainingDuration, messageAddAlteration.alteration.ownerId);

        if (messageAddAlteration.alteration.targetId == _myPlayer.Id)
            _myPlayer.Alterations.Add(alterationController);
        else
            FindPlayer(messageAddAlteration.alteration.targetId)?.Alterations.Add(alterationController);
    }

    private void OnMessageRefreshAlteration(MessageRefreshAlteration messageRefreshAlteration) {
        Alteration alteration = Resources.Load<Alteration>($"{SharedConfig.ALTERATIONS_PATH}/{messageRefreshAlteration.alteration.alterationName}");

        if (messageRefreshAlteration.alteration.targetId == _myPlayer.Id)
            _myPlayer.Alterations.Refresh(alteration, messageRefreshAlteration.alteration.remainingDuration, messageRefreshAlteration.alteration.ownerId);
        else
            FindPlayer(messageRefreshAlteration.alteration.targetId)?.Alterations.Refresh(alteration, messageRefreshAlteration.alteration.remainingDuration, messageRefreshAlteration.alteration.ownerId);
    }

    private void OnMessageRemoveAlteration(MessageRemoveAlteration messageRemoveAlteration) {
        Alteration alteration = Resources.Load<Alteration>($"{SharedConfig.ALTERATIONS_PATH}/{messageRemoveAlteration.alteration.alterationName}");

        if (messageRemoveAlteration.alteration.targetId == _myPlayer.Id)
            _myPlayer.Alterations.Remove(alteration, messageRemoveAlteration.alteration.ownerId);
        else
            FindPlayer(messageRemoveAlteration.alteration.targetId)?.Alterations.Remove(alteration, messageRemoveAlteration.alteration.ownerId);
    }

    private void Awake() {
        if (Current == null)
            Current = this;
        else
            Destroy(gameObject);
        MessageHandler.Current.OnMessageGameStateEvent += OnMessageGameState;
        MessageHandler.Current.OnMessageJoinedGameEvent += OnMessageJoinedGame;
        MessageHandler.Current.OnMessageLeftGameEvent += OnMessageLeftGame;
        MessageHandler.Current.OnMessagePlayerMovedEvent += OnMessagePlayerMoved;
        MessageHandler.Current.OnMessageHealthChangedEvent += OnMessageHealthChanged;
        MessageHandler.Current.OnMessageChannelEvent += OnMessageChannel;
        MessageHandler.Current.OnMessageCastEvent += OnMessageCast;
        MessageHandler.Current.OnMessageStopActivityEvent += OnMessageStopActivity;
        MessageHandler.Current.OnMessageEquipedEvent += OnMessageEquiped;
        MessageHandler.Current.OnMessageTriggerAnimationEvent += OnMessageTriggerAnimation;
        MessageHandler.Current.OnMessageAddAlterationEvent += OnMessageAddAlteration;
        MessageHandler.Current.OnMessageRefreshAlterationEvent += OnMessageRefreshAlteration;
        MessageHandler.Current.OnMessageRemoveAlterationEvent += OnMessageRemoveAlteration;
    }
}
