using UnityEngine;

[RequireComponent(typeof(PlayersGUI))]
[RequireComponent(typeof(InventoryGUI))]
[RequireComponent(typeof(CraftingGUI))]
public class GameGUI : MonoBehaviour
{
    private static GameGUI _current;
    [SerializeField]
    private GameObject _experiencePanel;
    [SerializeField]
    private GameObject _inventoryPanel;

    private void Awake() {
        if (_current == null)
            _current = this;
        else
            Destroy(gameObject);
    }

    private void CloseAll() {
        CloseCrafting();
        CloseInventory();
        CloseExperience();
    }

    private void ToggleExperience() {
        CloseCrafting();
        _experiencePanel.SetActive(!_experiencePanel.activeSelf);
    }

    private void ToggleInventory() {
        _inventoryPanel.SetActive(!_inventoryPanel.activeSelf);
    }

    public void ToggleEngineering() {
        CloseExperience();
        CraftingGUI.Current.ToggleEngineering();
    }

    public void ToggleAlchemy() {
        CloseExperience();
        CraftingGUI.Current.ToggleAlchemy();
    }

    public void ToggleForging() {
        CloseExperience();
        CraftingGUI.Current.ToggleForging();
    }

    public void ToggleCooking() {
        CloseExperience();
        CraftingGUI.Current.ToggleCooking();
    }

    public void CloseCrafting() => CraftingGUI.Current.CloseCrafting();

    public void CloseInventory() => _inventoryPanel.SetActive(false);

    public void CloseExperience() => _experiencePanel.SetActive(false);

    private void Update() {
        if (Input.GetKeyDown(KeyCode.B))
            ToggleInventory();
        if (Input.GetKeyDown(KeyCode.N))
            ToggleExperience();
        if (Input.GetKeyDown(KeyCode.F1))
            ToggleEngineering();
        if (Input.GetKeyDown(KeyCode.F2))
            ToggleAlchemy();
        if (Input.GetKeyDown(KeyCode.F3))
            ToggleForging();
        if (Input.GetKeyDown(KeyCode.F4))
            ToggleCooking();
        if (Input.GetKeyDown(KeyCode.Escape))
            CloseAll();
    }

    public static GameGUI Current => _current;
}