using UnityEngine;

public class AbilitySlot
{
    private Ability _ability;
    private float _currentCooldown;

    private event OnChangedHandler _onChanged;
    private event OnUpdatedHandler _onUpdated;

    public delegate void OnChangedHandler(Ability ability);
    public delegate void OnUpdatedHandler(float cooldown);

    public void Cooldown(float amount) {
        if (_currentCooldown > 0) {
            _currentCooldown = Mathf.Clamp(_currentCooldown - amount, 0, _ability.Cooldown);
            _onUpdated?.Invoke(_currentCooldown);
        }
    }

    public void Use() {
        if (_ability != null && _currentCooldown <= 0)
            TCPClient.Send(new MessageUseAbility(_ability.name));
    }

    public void Used() {
        _currentCooldown = _ability.Cooldown;
        _onUpdated?.Invoke(_currentCooldown);
    }

    public void Initialize(Ability ability) {
        _currentCooldown = 0;
        _ability = ability;
        _onChanged?.Invoke(_ability);
    }

    public event OnChangedHandler OnChanged {
        add => _onChanged += value;
        remove => _onChanged -= value;
    }

    public event OnUpdatedHandler OnUpdated {
        add => _onUpdated += value;
        remove => _onUpdated -= value;
    }

    public Ability Ability => _ability;
}