public class CharacterActivity
{
    public delegate void OnCastHandler(string activityName, float castTimeInSeconds);
    public delegate void OnChannelHandler(string activityName, int ticks, float intervalTimeInSeconds);
    public delegate void OnStopActivityHandler();
    public event OnCastHandler OnCast;
    public event OnChannelHandler OnChannel;
    public event OnStopActivityHandler OnStopActivity;

    public void Cast(string activityName, float castTimeInSeconds) {
        OnCast?.Invoke(activityName, castTimeInSeconds);
    }

    public void Channel(string activityName, int ticks, float intervalTimeInSeconds) {
        OnChannel?.Invoke(activityName, ticks, intervalTimeInSeconds);
    }

    public void StopActivity() {
        OnStopActivity?.Invoke();
    }
}