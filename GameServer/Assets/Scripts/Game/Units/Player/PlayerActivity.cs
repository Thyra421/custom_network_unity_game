using UnityEngine;
using System;

public class PlayerActivity : MonoBehaviour
{
    [SerializeField]
    private Player _player;
    private float _elapsedTime;
    private Action _currentActivity;
    private int _remainingTicks;
    private float _currentActivityTimeInSeconds;

    private void Process() {
        _elapsedTime += Time.deltaTime;
        if (_elapsedTime >= _currentActivityTimeInSeconds) {
            _currentActivity?.Invoke();
            _elapsedTime = 0;
            _remainingTicks--;
            if (_remainingTicks <= 0)
                Clear();
        }
    }

    private void Clear() {
        _currentActivity = null;
        _remainingTicks = 0;
        _currentActivityTimeInSeconds = 0;
        _elapsedTime = 0;
    }

    private void FixedUpdate() {
        if (_currentActivity != null)
            Process();
    }

    public void Cast(Action activity, string activityName, float castTimeInSeconds) {
        if (_player.Movement.IsMoving) {
            _player.Client.Tcp.Send(new MessageError(MessageErrorType.cantWhileMoving));
            return;
        }
        if (IsBusy) {
            _player.Client.Tcp.Send(new MessageError(MessageErrorType.busy));
            return;
        }

        Clear();
        _currentActivity = activity;
        _remainingTicks = 1;
        _currentActivityTimeInSeconds = castTimeInSeconds;
        _player.Room.PlayersManager.BroadcastTCP(new MessageCast(_player.Id, activityName, castTimeInSeconds));
    }

    public void Channel(Action activity, string activityName, int ticks, float intervalTimeInSeconds) {
        if (_player.Movement.IsMoving) {
            _player.Client.Tcp.Send(new MessageError(MessageErrorType.cantWhileMoving));
            return;
        }
        if (IsBusy) {
            _player.Client.Tcp.Send(new MessageError(MessageErrorType.busy));
            return;
        }

        Clear();
        _currentActivity = activity;
        _remainingTicks = ticks;
        _currentActivityTimeInSeconds = intervalTimeInSeconds;
        _player.Room.PlayersManager.BroadcastTCP(new MessageChannel(_player.Id, activityName, ticks, intervalTimeInSeconds));
    }

    public void Stop() {
        Clear();
        _player.Room.PlayersManager.BroadcastTCP(new MessageStopActivity(_player.Id));
    }

    public bool IsBusy => _currentActivity != null;
}