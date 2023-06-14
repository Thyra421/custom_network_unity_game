using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField]
    private GameObject _playerPrefab;
    [SerializeField]
    private NetworkGameObject _myPlayer;

    private void CreatePlayer(ObjectData player) {
        MainThreadWorker.Current.AddJob(() => {
            GameObject newPlayer = Instantiate(_playerPrefab, player.position.ToVector3(), Quaternion.identity);
            newPlayer.GetComponent<NetworkGameObject>().Id = player.id;
        });
    }

    private void OnServerMessageGameState(ServerMessageGameState messageGameState) {
        _myPlayer.Id = messageGameState.id;
        foreach (ObjectData p in messageGameState.players) {
            if (p.id == _myPlayer.Id)
                continue;
            CreatePlayer(p);
        }
    }

    private void OnServerMessageJoinedGame(ServerMessageJoinedGame messageJoinedGame) {
        if (messageJoinedGame.player.id != _myPlayer.Id)
            CreatePlayer(messageJoinedGame.player);
    }

    private void Start() {
        StartCoroutine(HTTPClient.Play(new ClientMessagePlay(NetworkManager.Secret), OnServerMessageGameState));        
        MessageHandler.Current.onServerMessageJoinedGame += OnServerMessageJoinedGame;
    }
}