using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Ability")]
public class Ability : ScriptableObject, IDisplayable, IRechargeable, IUsable
{
    [SerializeField]
    private string _displayName;
    [SerializeField]
    private float _cooldown;
    [SerializeField]
    private Sprite _icon;
    [SerializeField]
    private DirectEffect[] _effects;
    [SerializeField]
    private string _animationName;

    public string DisplayName => _displayName;

    public Sprite Icon => _icon;

    public DirectEffect[] Effects => _effects;

    public float Cooldown => _cooldown;

    public string AnimationName => _animationName;

}