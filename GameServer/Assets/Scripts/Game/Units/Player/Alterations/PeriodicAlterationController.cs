public class PeriodicAlterationController : AlterationController
{
    private float _nextTick;

    private bool TickReady => _remainingDuration <= _nextTick;

    public PeriodicAlterationController(Player player, Player owner, PeriodicAlteration alteration) : base(player, owner, alteration) {
        _nextTick = alteration.BaseDuration - alteration.IntervalDuration;
    }

    public override void Refresh() {
        base.Refresh();
        _nextTick = Alteration.BaseDuration - (Alteration as PeriodicAlteration).IntervalDuration;
    }

    public override void Update() {
        base.Update();
        if (TickReady) {
            new PlayerDirectEffectController(Player, Owner).Use(Alteration as PeriodicAlteration);
            _nextTick -= (Alteration as PeriodicAlteration).IntervalDuration;
        }
    }
}
