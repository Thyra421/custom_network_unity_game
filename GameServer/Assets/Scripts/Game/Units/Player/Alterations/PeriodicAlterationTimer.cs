public class PeriodicAlterationTimer : AlterationTimer
{
    private readonly Player _player;
    private float _nextTick;

    public PeriodicAlterationTimer(Player player, PeriodicAlteration alteration) : base(alteration) {
        _player = player;
        _nextTick = alteration.BaseDuration - alteration.IntervalDuration;
    }

    public override void Update() {
        base.Update();
        if (RemainingDuration <= _nextTick) {
            _player.EffectController.Use(Alteration as PeriodicAlteration);
            _nextTick -= (Alteration as PeriodicAlteration).IntervalDuration;
        }
    }
}
