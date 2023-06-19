using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _current;
    [SerializeField]
    //private GameObject _roomPrefab;

    public static GameManager Current {
        get => _current;
        set => _current = value;
    }

    //public void DestroyRoom(Room room) {
    //    Destroy(room.gameObject);
    //}

    //public Room CreateRoom() {
    //    return Instantiate(_roomPrefab).GetComponent<Room>();
    //}

    private void Awake() {
        if (_current == null)
            _current = this;
        else {
            Destroy(gameObject);
        }
    }

    void Start() {
        API.Start();
    }
}
