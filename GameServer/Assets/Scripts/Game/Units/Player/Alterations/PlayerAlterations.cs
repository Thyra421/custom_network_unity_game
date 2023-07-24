using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
        owner.Client.Tcp.Send(new MessageAddAlteration(newAlterationController.Data));
    }

    private void Refresh(AlterationController alterationController) {
        alterationController.Refresh();

        _player.Client.Tcp.Send(new MessageRefreshAlteration(alterationController.Data));
        alterationController.Owner.Client.Tcp.Send(new MessageRefreshAlteration(alterationController.Data));
    }

    private void Remove(AlterationController alterationController) {
        _player.Client.Tcp.Send(new MessageRemoveAlteration(alterationController.Data));
        alterationController.Owner.Client.Tcp.Send(new MessageRemoveAlteration(alterationController.Data));

        _alterationControllers.Remove(alterationController);
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

    public List<Alteration> Alterations => _alterationControllers.Select((AlterationController s) => s.Alteration).ToList();
}