using UnityEngine;

[RequireComponent(typeof(PlayersGUIManager))]
[RequireComponent(typeof(InventoryGUIManager))]
[RequireComponent(typeof(CraftingGUIManager))]
public class GameGUIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _experiencePanel;
    [SerializeField]
    private GameObject _inventoryPanel;

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
        CraftingGUIManager.Current.ToggleEngineering();
    }

    public void ToggleAlchemy() {
        CloseExperience();
        CraftingGUIManager.Current.ToggleAlchemy();
    }

    public void ToggleForging() {
        CloseExperience();
        CraftingGUIManager.Current.ToggleForging();
    }

    public void ToggleCooking() {
        CloseExperience();
        CraftingGUIManager.Current.ToggleCooking();
    }

    public void CloseCrafting() => CraftingGUIManager.Current.CloseCrafting();

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
}