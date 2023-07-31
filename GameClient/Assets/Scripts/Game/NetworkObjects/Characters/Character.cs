public abstract class Character : NetworkObject
{
    public CharacterAlterations Alterations { get; private set; }
    public CharacterHealth Statistics { get; private set; }
    public CharacterActivity Activity { get; private set; }
    protected abstract CharacterMovement CharacterMovement { get; }

    protected virtual void Awake() {
        Alterations = new CharacterAlterations();
        Statistics = new CharacterHealth();
        Activity = new CharacterActivity();
    }

    protected virtual void Update() {
        Alterations.Update();
    }

    protected virtual void FixedUpdate() {
        CharacterMovement.FixedUpdate();
    }
}
