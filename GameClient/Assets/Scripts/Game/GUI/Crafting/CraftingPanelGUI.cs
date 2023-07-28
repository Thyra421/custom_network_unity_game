using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftingPanelGUI : MonoBehaviour
{
    [SerializeField]
    private string _directoryName;
    [SerializeField]
    private Transform _selectionParent;
    [SerializeField]
    private Transform _reagentsParent;
    [SerializeField]
    private GameObject _itemSelectionTemplate;
    [SerializeField]
    private GameObject _reagentTemplate;
    [SerializeField]
    private ItemGUI _selectedItemGUI;
    [SerializeField]
    private TMP_Text _selectedItemTitleText;
    [SerializeField]
    private TMP_Text _selectedItemDescriptionText;
    [SerializeField]
    private Button _craftButton;
    private CraftingPattern _selectedPattern;

    private void OnClickCraft() {
        TCPClient.Send(new MessageCraft(_selectedPattern.name, _directoryName));
    }

    private void Start() {
        CraftingPattern[] patterns = Resources.LoadAll<CraftingPattern>($"{SharedConfig.Current.CraftingPattersPath}/{_directoryName}");
        foreach (CraftingPattern pattern in patterns) {
            CraftingItemSelectionGUI craftingItemSelectionGUI = Instantiate(_itemSelectionTemplate, _selectionParent).GetComponent<CraftingItemSelectionGUI>();
            craftingItemSelectionGUI.Initialize(this, pattern);
        }
        SelectPattern(patterns[0]);
    }

    private void Awake() {
        _craftButton.onClick.AddListener(OnClickCraft);
    }

    public void SelectPattern(CraftingPattern pattern) {
        if (pattern == _selectedPattern)
            return;
        _selectedItemTitleText.text = pattern.Outcome.Item.DisplayName;
        _selectedItemDescriptionText.text = pattern.Outcome.Item.Description;
        _selectedItemGUI.Initialize(pattern.Outcome.Item, pattern.Outcome.Amount);
        foreach (Transform child in _reagentsParent)
            Destroy(child.gameObject);
        foreach (ItemStack reagent in pattern.Reagents) {
            CraftingReagentGUI craftingReagentGUI = Instantiate(_reagentTemplate, _reagentsParent).GetComponent<CraftingReagentGUI>();
            craftingReagentGUI.Initialize(reagent.Item, reagent.Amount);
        }
        _selectedPattern = pattern;
    }
}