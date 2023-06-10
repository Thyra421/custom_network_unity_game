using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField]
    GameObject playerPrefab;

    void CreatePlayer(PlayerData player) {
        MainThreadWorker.current.AddJob(() => {
            GameObject newPlayer = Instantiate(playerPrefab, player.position.ToVector3(), Quaternion.identity);
            newPlayer.GetComponent<NetworkGameObject>().id = player.id;
        });
    }

    void OnJoinedWorld(ServerMessagePositions messagePositions) {
        foreach (PlayerData p in messagePositions.players) {
            if (p.id == NetworkManager.current.GetId)
                continue;
            CreatePlayer(p);
        }
    }

    void OnServerMessageJoinedGame(ServerMessageJoinedGame messageJoinedGame) {
        CreatePlayer(messageJoinedGame.player);
    }

    void OnServerMessagePositions(ServerMessagePositions messagePositions) {
        foreach (PlayerData p in messagePositions.players) {
            if (p.id == NetworkManager.current.GetId)
                continue;
            NetworkGameObject networkGameObject = NetworkGameObject.Find(p.id);
            networkGameObject.SetDestination(p.position.ToVector3());
        }
    }

    void Start() {
        StartCoroutine(NetworkManager.current.http.GetPlay(OnJoinedWorld));
        NetworkManager.current.tcp.onServerMessageJoinedGame += OnServerMessageJoinedGame;
        NetworkManager.current.udp.onServerMessagePositions += OnServerMessagePositions;
    }
}