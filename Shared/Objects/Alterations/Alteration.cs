using UnityEngine;

public abstract class Alteration : ScriptableObject
{
    [SerializeField]
    private string _displayName;
    [SerializeField]
    private Sprite _icon;
    [SerializeField]
    private bool _stackable;
    /// <summary>
    /// Set to -1 if permanent.
    /// </summary>
    [Tooltip("Set to -1 if permanent.")]
    [SerializeField]
    private float _baseDuration;
    [SerializeField]
    private bool _persistsAferDeath;

    public string DisplayName => _displayName;

    public Sprite Icon => _icon;

    public bool Stackable => _stackable;

    public float BaseDuration => _baseDuration;

    public bool PersistsAferDeath => _persistsAferDeath;
}