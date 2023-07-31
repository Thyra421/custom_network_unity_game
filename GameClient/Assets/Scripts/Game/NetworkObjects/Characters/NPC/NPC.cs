using UnityEngine;

public class NPC : Character
{
    public NPCAnimation Animation { get; private set; }
    public NPCMovement Movement { get; private set; }

    protected override CharacterMovement CharacterMovement => Movement;

    protected override void Awake() {
        base.Awake();
        Movement = new NPCMovement(this);
        Animation = new NPCAnimation(GetComponent<Animator>());
    }
}
