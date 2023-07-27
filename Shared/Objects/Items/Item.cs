using TMPro;
using UnityEngine;

public enum ItemRarity
{
    Common, Rare
}

public enum ItemProperty
{
    Stackable, NonStackable, Unique
}

public abstract class Item : ScriptableObject, IDisplayable, ITooltipHandler
{
    [SerializeField]
    protected string _displayName;
    [TextArea(minLines: 1, maxLines: 10)]
    [SerializeField]
    protected string _description;
    [SerializeField]
    protected ItemRarity _rarity;
    [SerializeField]
    protected ItemProperty _property;
    [SerializeField]
    protected Sprite _icon;

    public Sprite Icon => _icon;

    public ItemProperty Property => _property;

    public string DisplayName => _displayName;

    public string Description => _description;

    public ItemRarity Rarity => _rarity;

    public static Color RarityColor(ItemRarity rarity) {
        switch (rarity) {
            case ItemRarity.Common:
                return Color.white;
            case ItemRarity.Rare:
                return Color.blue;
        }
        return Color.white;
    }

    public virtual void BuildTooltip(RectTransform parent) {
        TooltipBuilder.BuildText(parent, DisplayName, RarityColor(_rarity), FontStyles.Bold);
        TooltipBuilder.BuildText(parent, $"\"{_description}\"", Color.yellow);
    }
}