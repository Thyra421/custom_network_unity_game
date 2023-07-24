public class PeriodicAlterationController : AlterationController
{
    private readonly Player _player;
    private float _nextTick;

    public PeriodicAlterationController(Player player, PeriodicAlteration alteration, Player owner) : base(alteration, owner) {
        _player = player;
        _nextTick = alteration.BaseDuration - alteration.IntervalDuration;
    }

    public override void Update() {
        base.Update();
        if (RemainingDuration <= _nextTick) {
            new PlayerDirectEffectController(_player, Owner).Use(Alteration as PeriodicAlteration);
            _nextTick -= (Alteration as PeriodicAlteration).IntervalDuration;
        }
    }
}
