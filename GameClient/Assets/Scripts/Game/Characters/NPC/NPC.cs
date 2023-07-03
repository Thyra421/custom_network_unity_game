using UnityEngine;

[RequireComponent(typeof(NPCMovement))]
public class NPC : Character
{
    private NPCMovement _movement;

    private void Awake() {
        _movement = gameObject.GetComponent<NPCMovement>();
    }

    public NPCMovement Movement => _movement;
}
