public abstract class CharacterHealth
{
    protected readonly Character _character;
    protected int _maxHealth = 100;
    protected int _currentHealth;

    public abstract int CurrentHealth { get; set; }
    public abstract int MaxHealth { get; set; }

    public CharacterHealth(Character character) {
        _character = character;
        _currentHealth = _maxHealth;
    }
}
