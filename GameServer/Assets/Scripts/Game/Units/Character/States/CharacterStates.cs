using System;
using System.Collections.Generic;
using System.Linq;

public class CharacterStates
{
    private readonly Character _character;
    private readonly StateController[] _states = new StateController[Enum.GetValues(typeof(StateType)).Length];

    public StateData[] Datas => _states.Select((StateController s) => s.Data).ToArray();

    private static StateData[] SelectDatas(List<StateController> states) => states.Select((StateController s) => s.Data).ToArray();

    public CharacterStates(Character character) {
        _character = character;
        for (int i = 0; i < Enum.GetValues(typeof(StateType)).Length; i++)
            _states[i] = new StateController((StateType)Enum.GetValues(typeof(StateType)).GetValue(i));
    }

    public StateController Find(StateType type) => Array.Find(_states, (StateController s) => s.Type == type);

    public void OnStatesChanged(List<StateController> modifiedStates) {
        if (_character is Player player)
            player.Client.TCP.Send(new MessageStatesChanged(SelectDatas(modifiedStates)));
    }
}