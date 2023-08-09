using System.Collections.Generic;
using UnityEngine;

public class PlayersManager : Singleton<PlayersManager>
{
    [SerializeField]
    private GameObject _playerPrefab;
    [SerializeField]
    private LocalPlayer _myPlayer;
    private readonly List<RemotePlayer> _remotePlayers = new List<RemotePlayer>();

    public LocalPlayer MyPlayer => _myPlayer;

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

            RemotePlayer remotePlayer = Find(pmd.id);

            if (remotePlayer != null) {
                remotePlayer.Movement.SetMovement(pmd.transform, pmd.movementSpeed);
                remotePlayer.Animation.SetAnimation(pmd.animation);
            }
        }
    }

    private void OnMessageLeftGame(MessageLeftGame messageLeftGame) {
        if (messageLeftGame.id == _myPlayer.Id)
            return;

        RemotePlayer remotePlayer = Find(messageLeftGame.id);

        if (remotePlayer != null)
            RemovePlayer(remotePlayer);
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

    private void OnMessageDash(MessageDash messageDash) {
        _myPlayer.Movement.ForceMovement(messageDash.destination.ToVector3, messageDash.speed);
    }

    protected override void Awake() {
        base.Awake();

        TCPClient.MessageRegistry.AddListener<MessageGameState>(OnMessageGameState);
        TCPClient.MessageRegistry.AddListener<MessageJoinedGame>(OnMessageJoinedGame);
        TCPClient.MessageRegistry.AddListener<MessageLeftGame>(OnMessageLeftGame);
        UDPClient.MessageRegistry.AddListener<MessagePlayersMoved>(OnMessagePlayersMoved);
        TCPClient.MessageRegistry.AddListener<MessageEquiped>(OnMessageEquiped);
        TCPClient.MessageRegistry.AddListener<MessageDash>(OnMessageDash);
    }

    public RemotePlayer Find(string id) => _remotePlayers.Find((RemotePlayer p) => p.Id == id);
}
