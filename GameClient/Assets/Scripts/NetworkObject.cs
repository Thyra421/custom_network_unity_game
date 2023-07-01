using UnityEngine;

public abstract class NetworkObject : MonoBehaviour
{
    protected string _id;

    public void Initialize(string id) {
        _id = id;
    }

    public string Id => _id;
}