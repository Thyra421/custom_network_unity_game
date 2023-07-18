using UnityEngine;

public class AbilitySlot
{
    private float _currentCooldown;

    public Ability CurrentAbility { get; private set; }

    public delegate void OnChangedHandler(Ability ability);
    public delegate void OnUpdatedHandler(float cooldown);
    public event OnChangedHandler OnChanged;
    public event OnUpdatedHandler OnUpdated;

    public void Cooldown(float amount) {
        if (_currentCooldown > 0) {
            _currentCooldown = Mathf.Clamp(_currentCooldown - amount, 0, CurrentAbility.Cooldown);
            OnUpdated?.Invoke(_currentCooldown);
        }
    }

    public void Use() {
        if (CurrentAbility != null && _currentCooldown <= 0)
            TCPClient.Send(new MessageUseAbility(CurrentAbility.name));
    }

    public void Used() {
        _currentCooldown = CurrentAbility.Cooldown;
        OnUpdated?.Invoke(_currentCooldown);
    }

    public void Initialize(Ability ability) {
        _currentCooldown = 0;
        CurrentAbility = ability;
        OnChanged?.Invoke(CurrentAbility);
    }
}