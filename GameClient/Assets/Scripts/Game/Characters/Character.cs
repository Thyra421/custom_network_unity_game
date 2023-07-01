using UnityEngine;

[RequireComponent(typeof(Statistics))]
public abstract class Character : NetworkObject
{
    [SerializeField]
    protected Statistics _statistics;
    [SerializeField]
    protected Animator _animator;
    private event OnCastHandler _onCastEvent;
    private event OnChannelHandler _onChannelEvent;
    private event OnStopActivityHandler _onStopActivityEvent;

    public delegate void OnCastHandler(string activityName, float castTimeInSeconds);
    public delegate void OnChannelHandler(string activityName, int ticks, float intervalTimeInSeconds);
    public delegate void OnStopActivityHandler();

    public void Cast(string activityName, float castTimeInSeconds) {
        _onCastEvent?.Invoke(activityName, castTimeInSeconds);
    }

    public void Channel(string activityName, int ticks, float intervalTimeInSeconds) {
        _onChannelEvent?.Invoke(activityName, ticks, intervalTimeInSeconds);
    }

    public void StopActivity() {
        _onStopActivityEvent?.Invoke();
    }

    public void TriggerAnimation(string animationName) {
        _animator.SetTrigger(animationName);
    }

    public Statistics Statistics => _statistics;

    public Animator Animator => _animator;

    public event OnCastHandler OnCastEvent {
        add => _onCastEvent += value;
        remove => _onCastEvent -= value;
    }

    public event OnChannelHandler OnChannelEvent {
        add => _onChannelEvent += value;
        remove => _onChannelEvent -= value;
    }

    public event OnStopActivityHandler OnStopActivityEvent {
        add => _onStopActivityEvent += value;
        remove => _onStopActivityEvent -= value;
    }
}
