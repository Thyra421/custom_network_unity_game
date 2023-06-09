using System.Linq;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField]
    GameObject playerPrefab;

    void OnJoinedWorld(HTTPResponse response) {
        Debug.Log(response.body);
    }

    void OnServerMessageJoinedGame(ServerMessageJoinedGame messageJoinedGame) {
        MainThreadWorker.current.AddJob(() => {
            GameObject newPlayer = Instantiate(playerPrefab);
            newPlayer.GetComponent<NetworkGameObject>().id = messageJoinedGame.player.id;
        });
    }

    void OnServerMessagePosition(ServerMessagePosition messagePosition) {
        foreach (PlayerData p in messagePosition.players) {
            if (p.id == NetworkManager.current.GetId)
                continue;
            NetworkGameObject networkGameObject = NetworkGameObject.Find(p.id);
            networkGameObject.SetDestination(p.position.ToVector3());
        }
    }

    void Start() {
        StartCoroutine(NetworkManager.current.http.Get($"play?id={NetworkManager.current.GetId}", OnJoinedWorld));
        NetworkManager.current.tcp.onServerMessageJoinedGame += OnServerMessageJoinedGame;
        NetworkManager.current.udp.onServerMessagePosition += OnServerMessagePosition;
    }
}