using UnityEngine;

public class AlterationController
{
    private float _remainingDuration;

    public Alteration Alteration { get; }
    public string OwnerId { get; }

    public delegate void OnRemainingDurationChangedHandler(float remainingDuration);
    public event OnRemainingDurationChangedHandler OnRemainingDurationChanged;

    public float RemainingDuration {
        get => _remainingDuration;
        private set {
            _remainingDuration = value;
            OnRemainingDurationChanged?.Invoke(_remainingDuration);
        }
    }

    public AlterationController(Alteration alteration, float remainingDuration, string ownerId) {
        Alteration = alteration;
        RemainingDuration = remainingDuration;
        OwnerId = ownerId;
    }

    public void Update() {
        if (Alteration.IsPermanent)
            return;
        RemainingDuration -= Time.deltaTime;
    }

    public void Refresh(float remainingDuration) {
        if (Alteration.IsPermanent)
            return;
        RemainingDuration = remainingDuration;
    }    
}