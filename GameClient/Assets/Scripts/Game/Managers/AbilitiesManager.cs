using UnityEngine;

public class AbilitiesManager : MonoBehaviour
{
    private static AbilitiesManager _current;
    private readonly AbilitySlot _abilitySlot1 = new AbilitySlot();
    private readonly AbilitySlot _abilitySlot2 = new AbilitySlot();
    private readonly AbilitySlot _abilitySlot3 = new AbilitySlot();
    private readonly AbilitySlot _abilitySlot4 = new AbilitySlot();
    private readonly AbilitySlot _abilitySlot5 = new AbilitySlot();

    public delegate void OnChangedWeaponHandler(Weapon weapon);
    public delegate void OnChangedPactHandler(Ability ability);
    public event OnChangedWeaponHandler OnChangedWeapon;
    public event OnChangedPactHandler OnChangedPact;

    private void Equip(Weapon weapon) {
        _abilitySlot1.Initialize(weapon.Abilities.Length > 0 ? weapon.Abilities[0] : null);
        _abilitySlot2.Initialize(weapon.Abilities.Length > 1 ? weapon.Abilities[1] : null);
        _abilitySlot3.Initialize(weapon.Abilities.Length > 2 ? weapon.Abilities[2] : null);
        _abilitySlot4.Initialize(weapon.Abilities.Length > 3 ? weapon.Abilities[3] : null);

        OnChangedWeapon?.Invoke(weapon);
    }

    public void Pact(Ability ability) {
        _abilitySlot5.Initialize(ability);

        OnChangedPact?.Invoke(ability);
    }

    private void OnMessageEquiped(MessageEquiped messageEquiped) {
        Weapon weapon = Resources.Load<Weapon>($"{SharedConfig.ITEMS_PATH}/{messageEquiped.weaponName}");

        Equip(weapon);
    }

    private void FixedUpdate() {
        float cooldownAmount = Time.deltaTime;
        _abilitySlot1.Cooldown(cooldownAmount);
        _abilitySlot2.Cooldown(cooldownAmount);
        _abilitySlot3.Cooldown(cooldownAmount);
        _abilitySlot4.Cooldown(cooldownAmount);
        _abilitySlot5.Cooldown(cooldownAmount);
    }

    private void Update() {
        if (_abilitySlot1.Ability != null && Input.GetKeyUp(KeyCode.Alpha1))
            _abilitySlot1.Use();
        if (_abilitySlot2.Ability != null && Input.GetKeyUp(KeyCode.Alpha2))
            _abilitySlot2.Use();
        if (_abilitySlot3.Ability != null && Input.GetKeyUp(KeyCode.Alpha3))
            _abilitySlot3.Use();
        if (_abilitySlot4.Ability != null && Input.GetKeyUp(KeyCode.Alpha4))
            _abilitySlot4.Use();
        if (_abilitySlot5.Ability != null && Input.GetKeyUp(KeyCode.Alpha5))
            _abilitySlot5.Use();
    }

    private void Awake() {
        if (_current == null)
            _current = this;
        else
            Destroy(gameObject);
        MessageHandler.Current.OnMessageEquipedEvent += OnMessageEquiped;
    }

    public void UseAbility(Ability ability) {
        if (_abilitySlot1.Ability == ability)
            _abilitySlot1.Used();
        if (_abilitySlot2.Ability == ability)
            _abilitySlot2.Used();
        if (_abilitySlot3.Ability == ability)
            _abilitySlot3.Used();
        if (_abilitySlot4.Ability == ability)
            _abilitySlot4.Used();
        if (_abilitySlot5.Ability == ability)
            _abilitySlot5.Used();
    }

    public static AbilitiesManager Current => _current;

    public AbilitySlot AbilitySlot1 => _abilitySlot1;

    public AbilitySlot AbilitySlot2 => _abilitySlot2;

    public AbilitySlot AbilitySlot3 => _abilitySlot3;

    public AbilitySlot AbilitySlot4 => _abilitySlot4;

    public AbilitySlot AbilitySlot5 => _abilitySlot5;
}