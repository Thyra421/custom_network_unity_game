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
        _remainingDuration = Alteration.BaseDurationInSeconds;
    }

    public virtual void Refresh() {
        if (Alteration.IsPermanent)
            return;
        _remainingDuration = Alteration.BaseDurationInSeconds;
    }

    public virtual void Update() {
        if (Alteration.IsPermanent)
            return;
        _remainingDuration = Mathf.Clamp(_remainingDuration - Time.deltaTime, 0, Alteration.BaseDurationInSeconds);
    }

    public AlterationData Data => new AlterationData(Player.Id, Owner.Id, Alteration.name, _remainingDuration);

    public bool IsExpired => _remainingDuration <= 0 && !Alteration.IsPermanent;
}
