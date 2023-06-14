using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField]
    private GameObject _playerPrefab;

    private void CreatePlayer(ObjectData player) {
        MainThreadWorker.Current.AddJob(() => {
            GameObject newPlayer = Instantiate(_playerPrefab, player.position.ToVector3(), Quaternion.identity);
            newPlayer.GetComponent<NetworkGameObject>().Id = player.id;
        });
    }

    private void OnServerMessageGameState(ServerMessageGameState messageGameState) {
        NetworkManager.Id = messageGameState.id;
        foreach (ObjectData p in messageGameState.players) {
            if (p.id == NetworkManager.Id)
                continue;
            CreatePlayer(p);
        }
    }

    private void OnServerMessageJoinedGame(ServerMessageJoinedGame messageJoinedGame) {
        if (messageJoinedGame.player.id != NetworkManager.Id)
            CreatePlayer(messageJoinedGame.player);
    }

    //private void OnServerMessagePositions(ServerMessagePositions messagePositions) {
    //    foreach (ObjectData p in messagePositions.players) {
    //        if (p.id == NetworkManager.Id)
    //            continue;
    //        NetworkGameObject networkGameObject = NetworkGameObject.Find(p.id);
    //        if (networkGameObject != null)
    //            networkGameObject.SetDestination(p.position.ToVector3());
    //    }
    //}

    //private void OnServerMessageLeftGame(ServerMessageLeftGame messageLeftGame) {
    //    NetworkGameObject networkGameObject = NetworkGameObject.Find(messageLeftGame.id);
    //    if (networkGameObject != null)
    //        Destroy(networkGameObject.gameObject);
    //}

    private void Start() {
        StartCoroutine(HTTPClient.Play(new ClientMessagePlay(NetworkManager.Secret), OnServerMessageGameState));
        MessageHandler.Current.onServerMessageJoinedGame += OnServerMessageJoinedGame;
        //MessageHandler.Current.onServerMessageLeftGame += OnServerMessageLeftGame;
        //MessageHandler.Current.onServerMessagePositions += OnServerMessagePositions;

    }
}