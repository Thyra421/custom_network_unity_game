using UnityEngine;

public class AbilitiesManager : Singleton<AbilitiesManager>
{
    public AbilitySlot AbilitySlot1 { get; } = new AbilitySlot();
    public AbilitySlot AbilitySlot2 { get; } = new AbilitySlot();
    public AbilitySlot AbilitySlot3 { get; } = new AbilitySlot();
    public AbilitySlot AbilitySlot4 { get; } = new AbilitySlot();
    public AbilitySlot AbilitySlot5 { get; } = new AbilitySlot();

    public delegate void OnChangedWeaponHandler(Weapon weapon);
    public delegate void OnChangedPactHandler(Ability ability);
    public event OnChangedWeaponHandler OnChangedWeapon;
    public event OnChangedPactHandler OnChangedPact;

    private void OnMessageUsedAbility(MessageUsedAbility messageUsedAbility) {
        Ability ability = Resources.Load<Ability>($"{SharedConfig.Current.AbilitiesPath}/{messageUsedAbility.abilityName}");

        UsedAbility(ability);
    }

    protected override void Awake() {
        base.Awake();

        TCPClient.MessageRegistry.AddListener<MessageUsedAbility>(OnMessageUsedAbility);
    }

    private void Update() {
        if (AbilitySlot1.CurrentAbility != null && Input.GetKeyUp(KeyCode.Alpha1))
            AbilitySlot1.Use();
        if (AbilitySlot2.CurrentAbility != null && Input.GetKeyUp(KeyCode.Alpha2))
            AbilitySlot2.Use();
        if (AbilitySlot3.CurrentAbility != null && Input.GetKeyUp(KeyCode.Alpha3))
            AbilitySlot3.Use();
        if (AbilitySlot4.CurrentAbility != null && Input.GetKeyUp(KeyCode.Alpha4))
            AbilitySlot4.Use();
        if (AbilitySlot5.CurrentAbility != null && Input.GetKeyUp(KeyCode.Alpha5))
            AbilitySlot5.Use();
    }

    private void FixedUpdate() {
        float cooldownAmount = Time.deltaTime;
        AbilitySlot1.Cooldown(cooldownAmount);
        AbilitySlot2.Cooldown(cooldownAmount);
        AbilitySlot3.Cooldown(cooldownAmount);
        AbilitySlot4.Cooldown(cooldownAmount);
        AbilitySlot5.Cooldown(cooldownAmount);
    }

    public void UsedAbility(Ability ability) {
        if (AbilitySlot1.CurrentAbility == ability)
            AbilitySlot1.Used();
        if (AbilitySlot2.CurrentAbility == ability)
            AbilitySlot2.Used();
        if (AbilitySlot3.CurrentAbility == ability)
            AbilitySlot3.Used();
        if (AbilitySlot4.CurrentAbility == ability)
            AbilitySlot4.Used();
        if (AbilitySlot5.CurrentAbility == ability)
            AbilitySlot5.Used();
    }

    public void Equip(Weapon weapon) {
        AbilitySlot1.Initialize(weapon.Abilities.Length > 0 ? weapon.Abilities[0] : null);
        AbilitySlot2.Initialize(weapon.Abilities.Length > 1 ? weapon.Abilities[1] : null);
        AbilitySlot3.Initialize(weapon.Abilities.Length > 2 ? weapon.Abilities[2] : null);
        AbilitySlot4.Initialize(weapon.Abilities.Length > 3 ? weapon.Abilities[3] : null);

        OnChangedWeapon?.Invoke(weapon);
    }

    public void Pact(Ability ability) {
        AbilitySlot5.Initialize(ability);

        OnChangedPact?.Invoke(ability);
    }
}