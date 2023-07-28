using UnityEngine;

[CreateAssetMenu(menuName = "Config/SharedConfig")]
public class SharedConfig : SingletonScriptableObject<SharedConfig>
{
    [SerializeField]
    private int _syncFrequency = 20;
    [SerializeField]
    private int _TCPBatchSize = 1024;
    [SerializeField]
    private int _inventorySpace = 30;
    [SerializeField]
    private float _playerMovementSpeed = 7;
    [SerializeField]
    private string _prefabsPath = "Shared/Prefabs";
    [SerializeField]
    private string _itemsPath = "Shared/Items";
    [SerializeField]
    private string _dropSourcesPath = "Shared/DropSources";
    [SerializeField]
    private string _craftingPattersPath = "Shared/CraftingPatterns";
    [SerializeField]
    private string _NPCsPath = "Shared/NPCs";
    [SerializeField]
    private string _abilitiesPath = "Shared/Abilities";
    [SerializeField]
    private string _alterationsPath = "Shared/Alterations";    

    public int SyncFrequency { get => _syncFrequency; }

    public int TCPBatchSize { get => _TCPBatchSize; }

    public int InventorySpace { get => _inventorySpace; }

    public float PlayerMovementSpeed { get => _playerMovementSpeed; }

    public string PrefabsPath { get => _prefabsPath; }

    public string ItemsPath { get => _itemsPath; }

    public string DropSourcesPath { get => _dropSourcesPath; }

    public string CraftingPattersPath { get => _craftingPattersPath; }

    public string NPCsPath { get => _NPCsPath; }

    public string AbilitiesPath { get => _abilitiesPath; }

    public string AlterationsPath { get => _alterationsPath; }
}