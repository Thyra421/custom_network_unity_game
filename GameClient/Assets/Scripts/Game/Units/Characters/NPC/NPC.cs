using UnityEngine;

[RequireComponent(typeof(RemoteUnitMovement))]
public class NPC : Character
{
    public override CharacterAnimation CharacterAnimation => Animation;
    public NPCAnimation Animation { get; private set; }
    public RemoteUnitMovement Movement { get; private set; }

    protected override void Awake() {
        base.Awake();
        Animation = new NPCAnimation(GetComponent<Animator>());
        Movement = GetComponent<RemoteUnitMovement>();
        _alterations = GetComponent<CharacterAlterations>();
    }
}
