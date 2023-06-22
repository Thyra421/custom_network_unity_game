using UnityEngine;

public enum ItemRarity
{
    Common, Rare
}

public enum ItemProperty
{
    Stackable, NonStackable, Unique
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
    private ItemProperty _property;
    [SerializeField]
    private Sprite _icon;

    public Sprite Icon => _icon;

    public ItemProperty Property => _property;
}