using UnityEngine;

[RequireComponent(typeof(NPCMovement))]
public class NPC : Character
{
    public override CharacterAnimation CharacterAnimation => Animation;
    public NPCAnimation Animation { get; private set; }
    public NPCMovement Movement { get; private set; }

    protected override void Awake() {
        base.Awake();
        Animation = new NPCAnimation(GetComponent<Animator>());
        Movement = GetComponent<NPCMovement>();
        _alterations = GetComponent<CharacterAlterations>();
    }
}
