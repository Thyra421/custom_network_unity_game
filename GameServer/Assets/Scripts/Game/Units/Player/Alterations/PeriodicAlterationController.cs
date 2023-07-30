using UnityEngine;

public class PeriodicAlterationController : AlterationController
{
    private float _tickTimer;

    private bool TickReady => _tickTimer <= 0;

    public PeriodicAlterationController(Player player, Player owner, PeriodicAlteration alteration) : base(player, owner, alteration) {
        _tickTimer = alteration.IntervalDurationInSeconds;
    }

    public override void Refresh() {
        base.Refresh();
        _tickTimer = (Alteration as PeriodicAlteration).IntervalDurationInSeconds;
    }

    public override void Update() {
        base.Update();
        _tickTimer -= Time.deltaTime;
        if (TickReady) {
            new PlayerDirectEffectController(Player, Owner).Use(Alteration as PeriodicAlteration);
            _tickTimer = (Alteration as PeriodicAlteration).IntervalDurationInSeconds;
        }
    }
}
