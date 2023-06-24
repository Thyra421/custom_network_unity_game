using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftingGUI : MonoBehaviour
{
    private static CraftingGUI _current;
    [SerializeField]
    private Transform _selectionParent;
    [SerializeField]
    private Transform _reagentsParent;
    [SerializeField]
    private GameObject _itemSelectionTemplate;
    [SerializeField]
    private GameObject _reagentTemplate;
    [SerializeField]
    private ItemGUI _itemGUI;
    [SerializeField]
    private TMP_Text _titleText;
    [SerializeField]
    private TMP_Text _descriptionText;
    [SerializeField]
    private Button _craftButton;
    private CraftingPattern _selectedPattern;

    private void OnClickCraft() {
        TCPClient.Send(new MessageCraft(_selectedPattern.name));
    }

    private void Start() {
        CraftingPattern[] patterns = Resources.LoadAll<CraftingPattern>(SharedConfig.CRAFTING_PATTERNS_PATH);
        foreach (CraftingPattern pattern in patterns) {
            CraftingItemSelectionGUI craftingItemSelectionGUI = Instantiate(_itemSelectionTemplate, _selectionParent).GetComponent<CraftingItemSelectionGUI>();
            craftingItemSelectionGUI.Initialize(pattern);
        }
        SelectPattern(patterns[0]);
    }

    private void Awake() {
        if (_current == null)
            _current = this;
        else
            Destroy(gameObject);
        _craftButton.onClick.AddListener(OnClickCraft);
    }

    public void SelectPattern(CraftingPattern pattern) {
        if (pattern == _selectedPattern)
            return;
        _titleText.text = pattern.Outcome.Item.DisplayName;
        _descriptionText.text = pattern.Outcome.Item.Description;
        _itemGUI.Initialize(pattern.Outcome.Item, pattern.Outcome.Amount);
        foreach (Transform child in _reagentsParent)
            Destroy(child.gameObject);
        foreach (ItemStack reagent in pattern.Reagents) {
            CraftingReagentGUI craftingReagentGUI = Instantiate(_reagentTemplate, _reagentsParent).GetComponent<CraftingReagentGUI>();
            craftingReagentGUI.Initialize(reagent.Item, reagent.Amount);
        }
        _selectedPattern = pattern;
    }

    public static CraftingGUI Current => _current;
}