using UnityEngine;

public abstract class Character : NetworkObject
{
    [SerializeField]
    protected Animator _animator;

    public Statistics Statistics { get; private set; }
    public CharacterAbilityEffectController AbilityEffectController { get; private set; }

    public delegate void OnCastHandler(string activityName, float castTimeInSeconds);
    public delegate void OnChannelHandler(string activityName, int ticks, float intervalTimeInSeconds);
    public delegate void OnStopActivityHandler();
    public event OnCastHandler OnCast;
    public event OnChannelHandler OnChannel;
    public event OnStopActivityHandler OnStopActivity;

    private void Awake() {
        Statistics = new Statistics();
        AbilityEffectController = new CharacterAbilityEffectController(this);
    }

    public void Cast(string activityName, float castTimeInSeconds) {
        OnCast?.Invoke(activityName, castTimeInSeconds);
    }

    public void Channel(string activityName, int ticks, float intervalTimeInSeconds) {
        OnChannel?.Invoke(activityName, ticks, intervalTimeInSeconds);
    }

    public void StopActivity() {
        OnStopActivity?.Invoke();
    }

    public void TriggerAnimation(string animationName) {
        _animator.SetTrigger(animationName);
    }

    public Animator Animator => _animator;
}
