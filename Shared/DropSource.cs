using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Drop Source")]
public class DropSource : ScriptableObject
{
    [SerializeField]
    private List<Item> _drops;
    [SerializeField]
    private GameObject _prefab;

    public GameObject Prefab => _prefab;

    public List<Item> Drops => _drops;
}