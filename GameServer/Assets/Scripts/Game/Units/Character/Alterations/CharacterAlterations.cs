using System.Collections.Generic;
using UnityEngine;

public class CharacterAlterations : MonoBehaviour
{
    [SerializeField]
    private Character _character;
    private readonly List<AlterationController> _alterationControllers = new List<AlterationController>();

    private AlterationController Find(Alteration alteration, Character owner) => _alterationControllers.Find((AlterationController ac) => ac.Alteration == alteration && ac.Owner == owner);

    private void Add(Alteration alteration, Character owner) {
        AlterationController newAlterationController;

        if (alteration is PeriodicAlteration periodicAlteration)
            newAlterationController = new PeriodicAlterationController(_character, owner, periodicAlteration);
        else
            newAlterationController = new AlterationController(_character, owner, alteration);

        _alterationControllers.Add(newAlterationController);

        if (_character is Player player)
            player.Client.TCP.Send(new MessageAddAlteration(newAlterationController.Data));
        if (owner is Player playerOwner && _character != owner)
            playerOwner.Client.TCP.Send(new MessageAddAlteration(newAlterationController.Data));

        if (alteration is ContinuousAlteration continuousAlteration)
            _character.Statistics.OnAddedContinuousAlteration(continuousAlteration);
    }

    private void Refresh(AlterationController alterationController) {
        alterationController.Refresh();

        if (_character is Player player)
            player.Client.TCP.Send(new MessageRefreshAlteration(alterationController.Data));
        if (alterationController.Owner is Player playerOwner && _character != alterationController.Owner)
            playerOwner.Client.TCP.Send(new MessageRefreshAlteration(alterationController.Data));
    }

    private void Remove(AlterationController alterationController) {
        if (_character is Player player)
            player.Client.TCP.Send(new MessageRemoveAlteration(alterationController.Data));
        if (alterationController.Owner is Player playerOwner && _character != alterationController.Owner)
            playerOwner.Client.TCP.Send(new MessageRemoveAlteration(alterationController.Data));

        _alterationControllers.Remove(alterationController);

        if (alterationController.Alteration is ContinuousAlteration continuousAlteration)
            _character.Statistics.OnRemovedContinuousAlteration(continuousAlteration);
    }

    private void FixedUpdate() {
        List<AlterationController> expiredAlterationControllers = new List<AlterationController>();

        foreach (AlterationController ac in _alterationControllers) {
            ac.Update();
            if (ac.IsExpired)
                expiredAlterationControllers.Add(ac);
        }

        expiredAlterationControllers.ForEach((AlterationController ac) => Remove(ac));
    }

    public void Remove(Alteration alteration, Character owner) {
        AlterationController alterationController = Find(alteration, owner);

        if (alterationController != null)
            Remove(alterationController);
    }

    public void Apply(Alteration alteration, Character owner) {
        AlterationController alterationController = Find(alteration, owner);

        if (alterationController != null)
            Refresh(alterationController);
        else
            Add(alteration, owner);
    }
}