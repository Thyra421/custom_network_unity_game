using System;

public class StatesManager : Singleton<StatesManager>
{
    private readonly State[] _states = new State[Enum.GetValues(typeof(StateType)).Length];

    public bool HasControl => !Find(StateType.Stunned).Value;

    private void OnMessageStatesChanged(MessageStatesChanged messageStatesChanged) {
        foreach (StateData sd in messageStatesChanged.stateDatas)
            Find(sd.type).Value = sd.value;
    }

    protected override void Awake() {
        base.Awake();

        for (int i = 0; i < Enum.GetValues(typeof(StateType)).Length; i++)
            _states[i] = new State((StateType)Enum.GetValues(typeof(StateType)).GetValue(i));

        TCPClient.MessageHandler.AddListener<MessageStatesChanged>(OnMessageStatesChanged);
    }

    public State Find(StateType type) => Array.Find(_states, (State s) => s.Type == type);
}