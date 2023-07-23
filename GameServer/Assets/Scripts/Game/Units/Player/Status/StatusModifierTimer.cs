using UnityEngine;

public class StatusModifierTimer
{
    public StatusModifier StatusModifier { get; private set; }
    public float RemainingDuration { get; private set; }

    public StatusModifierTimer(StatusModifier statusModifier) {
        StatusModifier = statusModifier;
        RemainingDuration = statusModifier.BaseDuration;
    }

    public void Update() {
        RemainingDuration -= Time.deltaTime;
    }
}
