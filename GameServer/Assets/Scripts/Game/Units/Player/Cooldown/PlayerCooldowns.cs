using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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