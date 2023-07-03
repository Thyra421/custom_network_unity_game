using UnityEngine;

public class Unit : MonoBehaviour
{
    protected readonly string _id = Utils.GenerateUUID();

    public string Id => _id;
}