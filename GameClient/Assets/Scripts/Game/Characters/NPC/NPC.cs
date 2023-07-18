using UnityEngine;

[RequireComponent(typeof(NPCMovement))]
public class NPC : Character
{
    public NPCMovement Movement { get; private set; }

    private void Awake() {
        Movement = gameObject.GetComponent<NPCMovement>();
    }
}
