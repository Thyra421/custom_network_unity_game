using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName="Ability/Aimed")]
public class AimedAbility : OffensiveAbility
{
    [SerializeField]
    private GameObject _prefab;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _distance;

    public GameObject Prefab => _prefab;

    public float Speed => _speed;

    public float Distance => _distance;

    public override void BuildTooltip(RectTransform parent) {
        base.BuildTooltip(parent);
        TooltipBuilder.Current.BuildText(parent, "Aimed");
    }
}