using UnityEngine;

[RequireComponent(typeof(NPCMovement))]
public class NPC : Character
{
    [SerializeField]
    private NPCMovement _movement;

    public NPCMovement Movement => _movement;
}
