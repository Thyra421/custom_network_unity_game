using UnityEngine;

public class AlterationController
{
    protected float _remainingDuration;

    public Character Character { get; }
    public Character Owner { get; }
    public Alteration Alteration { get; }
    public AlterationData Data => new AlterationData(Character.Id, Owner.Id, Alteration.name, _remainingDuration);
    public bool IsExpired => _remainingDuration <= 0 && !Alteration.IsPermanent;

    public AlterationController(Character player, Character owner, Alteration alteration) {
        Character = player;
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
}
