using UnityEngine;

[RequireComponent(typeof(NPCMovement))]
[RequireComponent(typeof(NPCAnimation))]
public class NPC : Character
{
    private NPCHealth _health;

    public NPCArea Area { get; private set; }
    public NPCMovement Movement { get; private set; }
    public NPCAnimation Animation { get; private set; }
    public NPCData Data => new NPCData(Id, TransformData, Animation.Data, Area.Animal.name);
    public override CharacterData CharacterData => new CharacterData(Id, CharacterType.NPC);

    public override CharacterHealth Health => _health;

    protected override void Awake() {
        base.Awake();
        Movement = GetComponent<NPCMovement>();
        Animation = GetComponent<NPCAnimation>();
        _alterations = GetComponent<CharacterAlterations>();
        _health = new NPCHealth(this);
    }

    public void Initialize(Room room, NPCArea area) {
        Initialize(room);
        Area = area;
        Movement.Initialize(this);
    }
}
