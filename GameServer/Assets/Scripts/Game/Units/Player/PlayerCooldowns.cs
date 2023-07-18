using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Cooldown
{
    public ICooldownHandler Element { get; private set; }
    public float RemainingCooldown { get; private set; }

    public Cooldown(ICooldownHandler element) {
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

    public bool Any(ICooldownHandler ability) => _cooldowns.Any((Cooldown c) => c.Element == ability);

    public void Add(ICooldownHandler cooldownHandler) {
        _cooldowns.Add(new Cooldown(cooldownHandler));
    }
}