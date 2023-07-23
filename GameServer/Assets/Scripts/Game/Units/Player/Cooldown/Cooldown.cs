using UnityEngine;

public class Cooldown
{
    public IRechargeable Element { get; private set; }
    public float RemainingCooldown { get; private set; }

    public Cooldown(IRechargeable element) {
        Element = element;
        RemainingCooldown = element.Cooldown;
    }

    public void Recharge() {
        RemainingCooldown -= Time.deltaTime;
    }
}
