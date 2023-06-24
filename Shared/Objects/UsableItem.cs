using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class UsableItem : Item
{
    [SerializeField]
    private UnityEvent[] _onUse;
}