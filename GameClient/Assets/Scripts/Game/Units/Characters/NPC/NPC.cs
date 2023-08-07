using UnityEngine;

[RequireComponent(typeof(RemoteCharacterMovement))]
public class NPC : Character
{
    public override CharacterAnimation CharacterAnimation => Animation;
    public NPCAnimation Animation { get; private set; }
    public RemoteCharacterMovement Movement { get; private set; }

    protected override void Awake() {
        base.Awake();
        Animation = new NPCAnimation(GetComponent<Animator>());
        Movement = GetComponent<RemoteCharacterMovement>();
        Movement.Initialize(this);
        _alterations = GetComponent<CharacterAlterations>();
    }
}
