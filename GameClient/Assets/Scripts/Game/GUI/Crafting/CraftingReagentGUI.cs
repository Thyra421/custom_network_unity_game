using TMPro;
using UnityEngine;

public class CraftingReagentGUI : MonoBehaviour
{
    [SerializeField]
    private ItemGUI _itemGUI;
    [SerializeField]
    private TMP_Text _nameText;

    public void Initialize(Item item, int amount) {
        _itemGUI.Initialize(item, amount);
        _nameText.text = item.name;
    }
}