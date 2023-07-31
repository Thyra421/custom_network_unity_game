using UnityEngine;

public class PeriodicAlterationController : AlterationController
{
    private float _tickTimer;

    private bool TickReady => _tickTimer <= 0;

    public PeriodicAlterationController(Character character, Character owner, PeriodicAlteration alteration) : base(character, owner, alteration) {
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
            new CharacterDirectEffectController(Character, Owner).Use(Alteration as PeriodicAlteration);
            _tickTimer = (Alteration as PeriodicAlteration).IntervalDurationInSeconds;
        }
    }
}
