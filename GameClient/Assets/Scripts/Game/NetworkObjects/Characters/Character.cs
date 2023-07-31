using UnityEngine;

[RequireComponent(typeof(CharacterAlterations))]
public abstract class Character : NetworkObject
{
    [SerializeField]
    protected CharacterAlterations _alterations;

    public CharacterHealth Statistics { get; private set; }
    public CharacterActivity Activity { get; private set; }
    public CharacterAlterations Alterations => _alterations;

    protected virtual void Awake() {
        Statistics = new CharacterHealth();
        Activity = new CharacterActivity();
    }
}
