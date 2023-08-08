using System;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(NPCMovement))]
[RequireComponent(typeof(NPCAnimation))]
public class NPC : Character
{
    private NPCHealth _health;

    public NPCArea Area { get; private set; }
    public NPCMovement Movement { get; private set; }
    public NPCAnimation Animation { get; private set; }
    public NPCData Data => new NPCData(Id, TransformData, Area.Animal.name);
    public override CharacterData CharacterData => new CharacterData(Id, CharacterType.NPC);
    public override CharacterHealth Health => _health;
    public Player Target { get; private set; }

    protected override void Awake() {
        base.Awake();

        Movement = GetComponent<NPCMovement>();
        Animation = GetComponent<NPCAnimation>();
        _alterations = GetComponent<CharacterAlterations>();
        _health = new NPCHealth(this);
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, 10);
    }

    private void Update() {
        if (Target != null)
            return;

        Collider[] colliders = Physics.OverlapSphere(transform.position, 10, LayerMask.GetMask("Player"));

        if (colliders.Length == 0)
            return;

        Collider collider = Array.Find(colliders, (Collider c) => c.TryGetComponent(out Player p) && p.Room == Room);

        if (collider != null)
            Target = collider.GetComponent<Player>();
    }

    public void Initialize(Room room, NPCArea area) {
        Initialize(room);
        Area = area;
        Movement.Initialize(this);
        _alterations.Initialize(this);
    }
}
