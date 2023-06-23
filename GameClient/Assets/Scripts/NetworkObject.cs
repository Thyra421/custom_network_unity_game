using UnityEngine;

public abstract class NetworkObject : MonoBehaviour
{
    protected string _id;

    public string Id => _id;
}