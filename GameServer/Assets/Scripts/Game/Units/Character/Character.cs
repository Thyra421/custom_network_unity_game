using UnityEngine;

[RequireComponent(typeof(CharacterAlterations))]
public abstract class Character : Unit
{
    [SerializeField]
    protected CharacterAlterations _alterations;

    public CharacterStatistics Statistics { get; private set; }
    public CharacterAlterations Alterations => _alterations;
    public abstract CharacterHealth Health { get; }
    public abstract CharacterData CharacterData { get; }

    protected virtual void Awake() {
        Statistics = new CharacterStatistics(this);
    }
}