using UnityEngine;

public abstract class NetworkObject : MonoBehaviour
{
    protected string _id;

    public string Id {
        get => _id;
        set => _id = value;
    }
}