using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerAlterations : MonoBehaviour
{
    [SerializeField]
    private Player _player;
    private List<AlterationController> _alterationControllers = new List<AlterationController>();

    private void FixedUpdate() {
        _alterationControllers.ForEach((AlterationController c) => c.Update());
        _alterationControllers.RemoveAll((AlterationController c) => c.RemainingDuration <= 0);
    }

    public void Add(Alteration alteration, Player owner) {
        if (alteration is PeriodicAlteration periodicAlteration)
            _alterationControllers.Add(new PeriodicAlterationController(_player, periodicAlteration, owner));
        else
            _alterationControllers.Add(new AlterationController(alteration, owner));
    }

    public List<Alteration> Alterations => _alterationControllers.Select((AlterationController s) => s.Alteration).ToList();
}