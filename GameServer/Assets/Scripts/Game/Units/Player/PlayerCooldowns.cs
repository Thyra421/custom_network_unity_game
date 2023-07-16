using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Cooldown
{
    private ICooldownHandler _element;
    private float _cooldown;

    public Cooldown(ICooldownHandler element) {
        _element = element;
        _cooldown = element.Cooldown;
    }

    public void Recharge() {
        _cooldown -= Time.deltaTime;
    }

    public float RemainingCooldown => _cooldown;

    public ICooldownHandler Element => _element;
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