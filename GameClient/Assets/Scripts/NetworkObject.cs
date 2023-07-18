using UnityEngine;

public abstract class NetworkObject : MonoBehaviour
{
    public string Id { get; private set; }

    public void Initialize(string id) {
        Id = id;
    }
}