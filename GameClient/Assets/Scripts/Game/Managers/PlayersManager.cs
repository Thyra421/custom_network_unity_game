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

    private RemotePlayer FindPlayer(string id) => _remotePlayers.Find((RemotePlayer p) => p.Id == id);

    private void CreatePlayer(PlayerData data) {
        RemotePlayer newRemotePlayer = Instantiate(_playerPrefab, data.transformData.position.ToVector3, Quaternion.identity).GetComponent<RemotePlayer>();
        newRemotePlayer.Initialize(data.id);
        _remotePlayers.Add(newRemotePlayer);
    }

    private void RemovePlayer(RemotePlayer remotePlayer) {
        _remotePlayers.Remove(remotePlayer);
        Destroy(remotePlayer.gameObject);
    }

    private void OnMessageGameState(MessageGameState messageGameState) {
        _myPlayer.Initialize(messageGameState.id);

        foreach (PlayerData p in messageGameState.players)
            if (p.id != _myPlayer.Id)
                CreatePlayer(p);
    }

    private void OnMessageJoinedGame(MessageJoinedGame messageJoinedGame) {
        if (messageJoinedGame.player.id != _myPlayer.Id)
            CreatePlayer(messageJoinedGame.player);
    }

    private void OnMessagePlayersMoved(MessagePlayersMoved messagePlayersMoved) {
        foreach (PlayerMovementData pmd in messagePlayersMoved.players) {
            if (pmd.id == _myPlayer.Id)
                continue;

            RemotePlayer remotePlayer = FindPlayer(pmd.id);

            if (remotePlayer != null) {
                remotePlayer.Movement.SetMovement(pmd.transformData, pmd.movementSpeed);
                remotePlayer.Animation.SetAnimation(pmd.animationData);
            }
        }
    }

    private void OnMessageLeftGame(MessageLeftGame messageLeftGame) {
        if (messageLeftGame.id == _myPlayer.Id)
            return;

        RemotePlayer remotePlayer = FindPlayer(messageLeftGame.id);

        if (remotePlayer != null)
            RemovePlayer(remotePlayer);
    }

    private void OnMessageChannel(MessageChannel messageChannel) {
        if (messageChannel.id == _myPlayer.Id)
            _myPlayer.Activity.Channel(messageChannel.activityName, messageChannel.ticks, messageChannel.intervalTimeInSeconds);
        else {
            RemotePlayer remotePlayer = FindPlayer(messageChannel.id);

            if (remotePlayer != null)
                remotePlayer.Activity.Channel(messageChannel.activityName, messageChannel.ticks, messageChannel.intervalTimeInSeconds);
        }
    }

    private void OnMessageCast(MessageCast messageCast) {
        if (messageCast.id == _myPlayer.Id)
            _myPlayer.Activity.Cast(messageCast.activityName, messageCast.castTimeInSeconds);
        else {
            RemotePlayer remotePlayer = FindPlayer(messageCast.id);

            if (remotePlayer != null)
                remotePlayer.Activity.Cast(messageCast.activityName, messageCast.castTimeInSeconds);
        }
    }

    private void OnMessageStopActivity(MessageStopActivity messageStopActivity) {
        if (messageStopActivity.id == _myPlayer.Id)
            _myPlayer.Activity.StopActivity();
        else {
            RemotePlayer remotePlayer = FindPlayer(messageStopActivity.id);

            if (remotePlayer != null)
                remotePlayer.Activity.StopActivity();
        }
    }

    private void OnMessageEquiped(MessageEquiped messageEquiped) {
        Weapon weapon = Resources.Load<Weapon>($"{SharedConfig.Current.ItemsPath}/{messageEquiped.weaponName}");

        if (messageEquiped.id == _myPlayer.Id) {
            AbilitiesManager.Current.Equip(weapon);
            // TODO equip weapon skin
        } else {
            // TODO equip weapon skin
        }
    }

    private void Awake() {
        if (Current == null)
            Current = this;
        else
            Destroy(gameObject);

        MessageHandler.Current.OnMessageGameStateEvent += OnMessageGameState;
        MessageHandler.Current.OnMessageJoinedGameEvent += OnMessageJoinedGame;
        MessageHandler.Current.OnMessageLeftGameEvent += OnMessageLeftGame;
        MessageHandler.Current.OnMessagePlayersMovedEvent += OnMessagePlayersMoved;
        MessageHandler.Current.OnMessageChannelEvent += OnMessageChannel;
        MessageHandler.Current.OnMessageCastEvent += OnMessageCast;
        MessageHandler.Current.OnMessageStopActivityEvent += OnMessageStopActivity;
        MessageHandler.Current.OnMessageEquipedEvent += OnMessageEquiped;
        MessageHandler.Current.OnMessageTriggerAnimationEvent += OnMessageTriggerAnimation;
        MessageHandler.Current.OnMessageAddAlterationEvent += OnMessageAddAlteration;
        MessageHandler.Current.OnMessageRefreshAlterationEvent += OnMessageRefreshAlteration;
        MessageHandler.Current.OnMessageRemoveAlterationEvent += OnMessageRemoveAlteration;
    }

    public void OnMessageTriggerAnimation(MessageTriggerAnimation messageTriggerAnimation) {
        if (messageTriggerAnimation.id == _myPlayer.Id)
            _myPlayer.Animation.SetTrigger(messageTriggerAnimation.animationName);
        else {
            RemotePlayer remotePlayer = FindPlayer(messageTriggerAnimation.id);

            if (remotePlayer != null)
                remotePlayer.Animation.SetTrigger(messageTriggerAnimation.animationName);
        }
    }

    public void OnMessageAddAlteration(MessageAddAlteration messageAddAlteration) {
        Alteration alteration = Resources.Load<Alteration>($"{SharedConfig.Current.AlterationsPath}/{messageAddAlteration.alteration.alterationName}");
        AlterationController alterationController = new AlterationController(alteration, messageAddAlteration.alteration.remainingDuration, messageAddAlteration.alteration.ownerId);

        if (messageAddAlteration.alteration.targetId == _myPlayer.Id)
            _myPlayer.Alterations.Add(alterationController);
        else {
            RemotePlayer remotePlayer = FindPlayer(messageAddAlteration.alteration.targetId);

            if (remotePlayer != null)
                remotePlayer.Alterations.Add(alterationController);
        }
    }

    public void OnMessageRefreshAlteration(MessageRefreshAlteration messageRefreshAlteration) {
        Alteration alteration = Resources.Load<Alteration>($"{SharedConfig.Current.AlterationsPath}/{messageRefreshAlteration.alteration.alterationName}");

        if (messageRefreshAlteration.alteration.targetId == _myPlayer.Id)
            _myPlayer.Alterations.Refresh(alteration, messageRefreshAlteration.alteration.remainingDuration, messageRefreshAlteration.alteration.ownerId);
        else {
            RemotePlayer remotePlayer = FindPlayer(messageRefreshAlteration.alteration.targetId);

            if (remotePlayer != null)
                remotePlayer.Alterations.Refresh(alteration, messageRefreshAlteration.alteration.remainingDuration, messageRefreshAlteration.alteration.ownerId);
        }
    }

    public void OnMessageRemoveAlteration(MessageRemoveAlteration messageRemoveAlteration) {
        Alteration alteration = Resources.Load<Alteration>($"{SharedConfig.Current.AlterationsPath}/{messageRemoveAlteration.alteration.alterationName}");

        if (messageRemoveAlteration.alteration.targetId == _myPlayer.Id)
            _myPlayer.Alterations.Remove(alteration, messageRemoveAlteration.alteration.ownerId);
        else {
            RemotePlayer remotePlayer = FindPlayer(messageRemoveAlteration.alteration.targetId);

            if (remotePlayer != null)
                remotePlayer.Alterations.Remove(alteration, messageRemoveAlteration.alteration.ownerId);
        }
    }

    public void OnMessageHealthChanged(MessageHealthChanged messageHealthChanged) {
        if (messageHealthChanged.character.id == _myPlayer.Id) {
            _myPlayer.Health.MaxHealth = messageHealthChanged.maxHealth;
            _myPlayer.Health.CurrentHealth = messageHealthChanged.currentHealth;
        } else {
            RemotePlayer remotePlayer = FindPlayer(messageHealthChanged.character.id);

            if (remotePlayer != null) {
                remotePlayer.Health.MaxHealth = messageHealthChanged.maxHealth;
                remotePlayer.Health.CurrentHealth = messageHealthChanged.currentHealth;
            }
        }
    }
}
