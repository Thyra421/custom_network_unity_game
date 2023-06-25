public abstract class Player : NetworkObject
{
    private event OnCastHandler _onCastEvent;
    private event OnChannelHandler _onChannelEvent;
    private event OnStopActivityHandler _onStopActivityEvent;

    public delegate void OnCastHandler(string activityName, float castTimeInSeconds);
    public delegate void OnChannelHandler(string activityName, int ticks, float intervalTimeInSeconds);
    public delegate void OnStopActivityHandler();

    public void Cast(string activityName, float castTimeInSeconds) {
        _onCastEvent(activityName, castTimeInSeconds);
    }

    public void Channel(string activityName, int ticks, float intervalTimeInSeconds) {
        _onChannelEvent(activityName, ticks, intervalTimeInSeconds);
    }

    public void StopActivity() {
        _onStopActivityEvent();
    }

    public abstract PlayerStatistics Statistics {
        get;
    }

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
