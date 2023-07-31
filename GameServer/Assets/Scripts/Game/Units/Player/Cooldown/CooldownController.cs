using UnityEngine;

public class CooldownController
{
    public IRechargeable Element { get; private set; }
    private float _remainingCooldownInSeconds;

    public bool IsReady => _remainingCooldownInSeconds <= 0;

    public CooldownController(IRechargeable element) {
        Element = element;
        _remainingCooldownInSeconds = element.CooldownInSeconds;
    }

    public void Recharge() {
        _remainingCooldownInSeconds -= Time.deltaTime;
    }
}
