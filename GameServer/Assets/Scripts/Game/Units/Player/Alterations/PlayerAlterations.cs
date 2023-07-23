using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerAlterations : MonoBehaviour
{
    private List<AlterationTimer> _alterationTimers = new List<AlterationTimer>();

    private void FixedUpdate() {
        _alterationTimers.ForEach((AlterationTimer t) => t.Update());
        _alterationTimers.RemoveAll((AlterationTimer t) => t.RemainingDuration <= 0);
    }

    public void Add(Alteration alteration) {
        _alterationTimers.Add(new AlterationTimer(alteration));
    }

    public List<Alteration> Alterations => _alterationTimers.Select((AlterationTimer s) => s.Alteration).ToList();
}