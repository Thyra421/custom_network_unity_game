using UnityEngine;

[System.Serializable]
public class ItemStack
{
    [SerializeField]
    private Item _item;
    [SerializeField]
    private int _amount;

    public Item Item => _item;

    public int Amount => _amount;
}
