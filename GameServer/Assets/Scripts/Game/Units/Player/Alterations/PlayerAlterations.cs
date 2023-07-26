using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class PlayerAlterations : MonoBehaviour
{
    [SerializeField]
    private Player _player;
    private readonly List<AlterationController> _alterationControllers = new List<AlterationController>();

    private void Add(Alteration alteration, Player owner) {
        AlterationController newAlterationController;

        if (alteration is PeriodicAlteration periodicAlteration)
            newAlterationController = new PeriodicAlterationController(_player, owner, periodicAlteration);
        else
            newAlterationController = new AlterationController(_player, owner, alteration);

        _alterationControllers.Add(newAlterationController);

        _player.Client.Tcp.Send(new MessageAddAlteration(newAlterationController.Data));
        if (_player != owner)
            owner.Client.Tcp.Send(new MessageAddAlteration(newAlterationController.Data));

        if (alteration is ContinuousAlteration continuousAlteration)
            _player.Statistics.OnAddedContinuousAlteration(continuousAlteration);
    }

    private void Refresh(AlterationController alterationController) {
        alterationController.Refresh();

        _player.Client.Tcp.Send(new MessageRefreshAlteration(alterationController.Data));
        if (_player != alterationController.Owner)
            alterationController.Owner.Client.Tcp.Send(new MessageRefreshAlteration(alterationController.Data));
    }

    private void Remove(AlterationController alterationController) {
        _player.Client.Tcp.Send(new MessageRemoveAlteration(alterationController.Data));
        if (_player != alterationController.Owner)
            alterationController.Owner.Client.Tcp.Send(new MessageRemoveAlteration(alterationController.Data));

        _alterationControllers.Remove(alterationController);

        if (alterationController.Alteration is ContinuousAlteration continuousAlteration)
            _player.Statistics.OnRemovedContinuousAlteration(continuousAlteration);
    }

    private AlterationController Find(Alteration alteration, Player owner) => _alterationControllers.Find((AlterationController ac) => ac.Alteration == alteration && ac.Owner == owner);

    private void FixedUpdate() {
        List<AlterationController> expiredAlterationControllers = new List<AlterationController>();

        foreach (AlterationController ac in _alterationControllers) {
            ac.Update();
            if (ac.IsExpired)
                expiredAlterationControllers.Add(ac);
        }

        expiredAlterationControllers.ForEach((AlterationController ac) => Remove(ac));
    }

    public void Apply(Alteration alteration, Player owner) {
        AlterationController alterationController = Find(alteration, owner);

        if (alterationController != null)
            Refresh(alterationController);
        else
            Add(alteration, owner);
    }
}