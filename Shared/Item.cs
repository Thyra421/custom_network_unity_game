using UnityEngine;

public enum ItemRarity
{
    Common, Rare
}

public abstract class Item : ScriptableObject
{
    [SerializeField]
    protected string _displayName;
    [SerializeField]
    protected string _description;
    [SerializeField]
    protected ItemRarity _rarity;
    [SerializeField]
    protected int _price;
    [SerializeField]
    protected bool _stackable;
    [SerializeField]
    protected bool _unique;
    [SerializeField]
    private GameObject _prefab;

    public GameObject Prefab => _prefab;
}