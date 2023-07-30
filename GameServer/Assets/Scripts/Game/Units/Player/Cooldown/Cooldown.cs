using UnityEngine;

public class Cooldown
{
    public IRechargeable Element { get; private set; }
    private float _remainingCooldownInSeconds;

    public Cooldown(IRechargeable element) {
        Element = element;
        _remainingCooldownInSeconds = element.CooldownInSeconds;
    }

    public void Recharge() {
        _remainingCooldownInSeconds -= Time.deltaTime;
    }

    public bool IsReady => _remainingCooldownInSeconds <= 0;
}
