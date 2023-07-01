using UnityEngine;

public class CraftingGUI : MonoBehaviour
{
    private static CraftingGUI _current;
    [SerializeField]
    private GameObject _engineeringPanel;
    [SerializeField]
    private GameObject _alchemyPanel;
    [SerializeField]
    private GameObject _forgingPanel;
    [SerializeField]
    private GameObject _cookingPanel;
    [SerializeField]
    private GameObject _craftingPanel;

    private void Awake() {
        if (_current == null)
            _current = this;
        else
            Destroy(gameObject);
    }

    public void ToggleEngineering() {
        if (_engineeringPanel.activeSelf && _craftingPanel.activeSelf) {
            _craftingPanel.SetActive(false);
            return;
        }
        _craftingPanel.SetActive(true);
        _engineeringPanel.SetActive(true);
        _alchemyPanel.SetActive(false);
        _forgingPanel.SetActive(false);
        _cookingPanel.SetActive(false);
    }

    public void ToggleAlchemy() {
        if (_alchemyPanel.activeSelf && _craftingPanel.activeSelf) {
            _craftingPanel.SetActive(false);
            return;
        }
        _craftingPanel.SetActive(true);
        _engineeringPanel.SetActive(false);
        _alchemyPanel.SetActive(true);
        _forgingPanel.SetActive(false);
        _cookingPanel.SetActive(false);
    }

    public void ToggleForging() {
        if (_forgingPanel.activeSelf && _craftingPanel.activeSelf) {
            _craftingPanel.SetActive(false);
            return;
        }
        _craftingPanel.SetActive(true);
        _engineeringPanel.SetActive(false);
        _alchemyPanel.SetActive(false);
        _forgingPanel.SetActive(true);
        _cookingPanel.SetActive(false);
    }

    public void ToggleCooking() {
        if (_cookingPanel.activeSelf && _craftingPanel.activeSelf) {
            _craftingPanel.SetActive(false);
            return;
        }
        _craftingPanel.SetActive(true);
        _engineeringPanel.SetActive(false);
        _alchemyPanel.SetActive(false);
        _forgingPanel.SetActive(false);
        _cookingPanel.SetActive(true);
    }

    public void CloseCrafting() {
        _craftingPanel.SetActive(false);
    }

    public static CraftingGUI Current => _current;
}