using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerCooldowns : MonoBehaviour
{
    private readonly List<CooldownController> _cooldowns = new List<CooldownController>();

    private void FixedUpdate() {
        List<CooldownController> cooldownsReady = new List<CooldownController>();

        foreach (CooldownController c in _cooldowns) {
            c.Recharge();
            if (c.IsReady)
                cooldownsReady.Add(c);
        }

        cooldownsReady.ForEach((CooldownController c) => _cooldowns.Remove(c));
    }

    public bool Any(IRechargeable ability) => _cooldowns.Any((CooldownController c) => c.Element == ability);

    public void Add(IRechargeable cooldownHandler) {
        _cooldowns.Add(new CooldownController(cooldownHandler));
    }
}