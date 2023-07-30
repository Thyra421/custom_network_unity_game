using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerCooldowns : MonoBehaviour
{
    private readonly List<Cooldown> _cooldowns = new List<Cooldown>();

    private void FixedUpdate() {
        List<Cooldown> cooldownsReady = new List<Cooldown>();

        foreach (Cooldown c in _cooldowns) {
            c.Recharge();
            if (c.IsReady)
                cooldownsReady.Add(c);
        }

        cooldownsReady.ForEach((Cooldown c) => _cooldowns.Remove(c));
    }

    public bool Any(IRechargeable ability) => _cooldowns.Any((Cooldown c) => c.Element == ability);

    public void Add(IRechargeable cooldownHandler) {
        _cooldowns.Add(new Cooldown(cooldownHandler));
    }
}