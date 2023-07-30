using UnityEngine;

public abstract class Alteration : ScriptableObject, IDisplayable
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
    private float _baseDurationInSeconds;
    [SerializeField]
    private bool _persistsAferDeath;

    public string DisplayName => _displayName;

    public Sprite Icon => _icon;

    public bool Stackable => _stackable;

    public float BaseDurationInSeconds => _baseDurationInSeconds;

    public bool PersistsAferDeath => _persistsAferDeath;

    public bool IsPermanent => _baseDurationInSeconds < 0;
}