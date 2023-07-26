using UnityEngine;

[CreateAssetMenu]
public class DropSource : ScriptableObject
{
    [SerializeField]
    private LootTable _lootTable;
    [SerializeField]
    private GameObject _prefab;
    [SerializeField]
    private int _minInclusive;
    [SerializeField]
    private int _minExclusive;
    /// <summary>
    /// Set to -1 if never respawn.
    /// </summary>
    [Tooltip("Set to -1 if never respawn.")]
    [SerializeField]
    private float _respawnTimerInSeconds;
    [SerializeField]
    private ExperienceType _experienceType;
    [SerializeField]
    private int _requiredLevel;

    public Item RandomLoot => _lootTable.RandomLoot;

    public GameObject Prefab => _prefab;

    public LootTable Drops => _lootTable;

    public int MinInclusive => _minInclusive;

    public int MaxExclusive => _minExclusive;

    public float RespawnTimerInSeconds => _respawnTimerInSeconds;

    public ExperienceType ExperienceType => _experienceType;
}