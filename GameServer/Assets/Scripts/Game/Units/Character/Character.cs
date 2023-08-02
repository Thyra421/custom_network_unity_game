using UnityEngine;

[RequireComponent(typeof(CharacterAlterations))]
public abstract class Character : Unit
{
    [SerializeField]
    protected CharacterAlterations _alterations;

    public CharacterStatistics Statistics { get; private set; }
    public CharacterStates States { get; private set; }
    public CharacterAlterations Alterations => _alterations;
    public CharacterPersistentEffectController PersistentEffectController { get; private set; }
    public CharacterDirectEffectController DirectEffectController { get; private set; }
    public abstract CharacterHealth Health { get; }
    public abstract CharacterData CharacterData { get; }

    protected virtual void Awake() {
        Statistics = new CharacterStatistics(this);
        States = new CharacterStates(this);
        PersistentEffectController = new CharacterPersistentEffectController(this);
        DirectEffectController = new CharacterDirectEffectController(this);
    }
}