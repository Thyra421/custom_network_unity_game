using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    private List<StatusModifierTimer> _statusModifierTimers = new List<StatusModifierTimer>();

    private void Update() {
        _statusModifierTimers.ForEach((StatusModifierTimer t) => t.Update());
        _statusModifierTimers.RemoveAll((StatusModifierTimer t) => t.RemainingDuration <= 0);
    }

    public void Add(StatusModifier statusModifier) {
        _statusModifierTimers.Add(new StatusModifierTimer(statusModifier));
    }

    public List<StatusModifier> StatusModifiers => _statusModifierTimers.Select((StatusModifierTimer s) => s.StatusModifier).ToList();
}