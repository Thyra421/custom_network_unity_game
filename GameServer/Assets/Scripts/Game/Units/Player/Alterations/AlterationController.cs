using UnityEngine;

public class AlterationController
{
    protected float _remainingDuration;

    public Player Player { get; }
    public Player Owner { get; }
    public Alteration Alteration { get; }

    public AlterationController(Player player, Player owner, Alteration alteration) {
        Player = player;
        Owner = owner;
        Alteration = alteration;
        _remainingDuration = Alteration.BaseDuration;
    }

    public virtual void Refresh() {
        _remainingDuration = Alteration.BaseDuration;
    }

    public virtual void Update() {
        _remainingDuration = Mathf.Clamp(_remainingDuration - Time.deltaTime, 0, Alteration.BaseDuration);
    }

    public AlterationData Data => new AlterationData(Player.Id, Owner.Id, Alteration.name, _remainingDuration);

    public bool IsExpired => _remainingDuration <= 0;
}
