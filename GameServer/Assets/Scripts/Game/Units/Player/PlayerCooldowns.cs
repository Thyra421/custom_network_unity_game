using System.Collections.Generic;
using System.Linq;
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

public class PlayerCooldowns : MonoBehaviour
{
    private readonly List<Cooldown> _cooldowns = new List<Cooldown>();

    private void FixedUpdate() {
        _cooldowns.ForEach((Cooldown c) => c.Recharge());
        _cooldowns.RemoveAll((Cooldown c) => c.RemainingCooldown <= 0);
    }

    public bool Any(IRechargeable ability) => _cooldowns.Any((Cooldown c) => c.Element == ability);

    public void Add(IRechargeable cooldownHandler) {
        _cooldowns.Add(new Cooldown(cooldownHandler));
    }
}