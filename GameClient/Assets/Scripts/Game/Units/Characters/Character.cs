using UnityEngine;

[RequireComponent(typeof(CharacterAlterations))]
public abstract class Character : Unit
{
    [SerializeField]
    protected CharacterAlterations _alterations;

    public CharacterHealth Health { get; private set; }
    public CharacterActivity Activity { get; private set; }
    public CharacterAlterations Alterations => _alterations;
    public abstract CharacterAnimation CharacterAnimation { get; }

    protected virtual void Awake() {
        Health = new CharacterHealth();
        Activity = new CharacterActivity();
    }
}
