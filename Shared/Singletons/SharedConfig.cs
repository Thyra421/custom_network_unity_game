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

    public int SyncFrequency => _syncFrequency;

    public int TCPBatchSize => _TCPBatchSize;

    public int InventorySpace => _inventorySpace;

    public float PlayerMovementSpeed => _playerMovementSpeed;

    public string PrefabsPath => _prefabsPath;

    public string ItemsPath => _itemsPath;

    public string DropSourcesPath => _dropSourcesPath;

    public string CraftingPattersPath => _craftingPattersPath;

    public string NPCsPath => _NPCsPath;

    public string AbilitiesPath => _abilitiesPath;

    public string AlterationsPath => _alterationsPath;
}