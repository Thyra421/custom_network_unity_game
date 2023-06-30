using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftingItemSelectionGUI : MonoBehaviour
{
    [SerializeField]
    private Button _selectButton;
    [SerializeField]
    private ItemGUI _itemGUI;
    [SerializeField]
    private TMP_Text _nameText;
    private CraftingPattern _craftingPattern;
    private CraftingPanelGUI _craftingGUI;

    private void OnClickSelectItem() {
        _craftingGUI.SelectPattern(_craftingPattern);
    }

    private void Awake() {
        _selectButton.onClick.AddListener(OnClickSelectItem);
    }

    public void Initialize(CraftingPanelGUI craftingGUI, CraftingPattern pattern) {
        _craftingGUI = craftingGUI;
        _itemGUI.Initialize(pattern.Outcome.Item, pattern.Outcome.Amount);
        _nameText.text = pattern.Outcome.Item.name;
        _craftingPattern = pattern;
    }
}