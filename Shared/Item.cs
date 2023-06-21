using UnityEngine;

public enum ItemRarity
{
    Common, Rare
}

public abstract class Item : ScriptableObject
{
    [SerializeField]
    protected string _displayName;
    [TextArea(minLines: 1, maxLines: 10)]
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
    protected Sprite _icon;
}