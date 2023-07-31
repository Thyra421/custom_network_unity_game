using UnityEngine;

[RequireComponent(typeof(NPCMovement))]
[RequireComponent(typeof(NPCAnimation))]
public class NPC : Unit
{
    public NPCArea Area { get; private set; }
    public NPCMovement Movement { get; private set; }
    public NPCAnimation Animation { get; private set; }
    public NPCData Data => new NPCData(Id, TransformData, Animation.Data, Area.Animal.name);

    private void Awake() {
        Movement = GetComponent<NPCMovement>();
        Animation = GetComponent<NPCAnimation>();
    }

    public void Initialize(NPCArea area) {
        Area = area;
        Movement.Initialize(this);
    }
}
