using UnityEngine;

public class AlterationTimer
{
    public Alteration Alteration { get; private set; }
    public float RemainingDuration { get; private set; }

    public AlterationTimer(Alteration alteration) {
        Alteration = alteration;
        RemainingDuration = alteration.BaseDuration;
    }

    public virtual void Update() {
        RemainingDuration -= Time.deltaTime;
    }
}
