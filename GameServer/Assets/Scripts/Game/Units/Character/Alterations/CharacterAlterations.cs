using System.Collections.Generic;
using UnityEngine;

public class CharacterAlterations : MonoBehaviour
{
    [SerializeField]
    private Character _character;
    private readonly List<AlterationController> _alterationControllers = new List<AlterationController>();

    private AlterationController Find(Alteration alteration, Character owner) => _alterationControllers.Find((AlterationController ac) => ac.Alteration == alteration && ac.Owner == owner);

    private PeriodicAlterationController AddPeriodicAlteration(PeriodicAlteration periodicAlteration, Character owner) {
        PeriodicAlterationController newPeriodicAlterationController = new PeriodicAlterationController(_character, owner, periodicAlteration);
        _alterationControllers.Add(newPeriodicAlterationController);

        return newPeriodicAlterationController;
    }

    private AlterationController AddContinuousAlteration(ContinuousAlteration continuousAlteration, Character owner) {
        AlterationController newAlterationController = new AlterationController(_character, owner, continuousAlteration);
        _alterationControllers.Add(newAlterationController);

        List<Statistic> modifiedStatistics = new List<Statistic>();
        _character.StatusEffectController.Add(continuousAlteration, modifiedStatistics);
        if (modifiedStatistics.Count > 0)
            _character.Statistics.OnStatisticsChanged(modifiedStatistics);

        return newAlterationController;
    }

    private void Add(Alteration alteration, Character owner) {
        AlterationController alterationController = null;

        if (alteration is PeriodicAlteration periodicAlteration)
            alterationController = AddPeriodicAlteration(periodicAlteration, owner);
        else if (alteration is ContinuousAlteration continuousAlteration)
            alterationController = AddContinuousAlteration(continuousAlteration, owner);

        if (_character is Player player)
            player.Client.TCP.Send(new MessageAddAlteration(alterationController.Data));
        if (owner is Player playerOwner && _character != owner)
            playerOwner.Client.TCP.Send(new MessageAddAlteration(alterationController.Data));
    }

    private void Refresh(AlterationController alterationController) {
        alterationController.Refresh();

        if (_character is Player player)
            player.Client.TCP.Send(new MessageRefreshAlteration(alterationController.Data));
        if (alterationController.Owner is Player playerOwner && _character != alterationController.Owner)
            playerOwner.Client.TCP.Send(new MessageRefreshAlteration(alterationController.Data));
    }

    private void OnRemovedContinuousAlteration(ContinuousAlteration continuousAlteration) {
        List<Statistic> modifiedStatistics = new List<Statistic>();

        _character.StatusEffectController.Remove(continuousAlteration, modifiedStatistics);
        if (modifiedStatistics.Count > 0)
            _character.Statistics.OnStatisticsChanged(modifiedStatistics);
    }

    private void Remove(AlterationController alterationController) {
        if (_character is Player player)
            player.Client.TCP.Send(new MessageRemoveAlteration(alterationController.Data));
        if (alterationController.Owner is Player playerOwner && _character != alterationController.Owner)
            playerOwner.Client.TCP.Send(new MessageRemoveAlteration(alterationController.Data));

        _alterationControllers.Remove(alterationController);

        if (alterationController.Alteration is ContinuousAlteration continuousAlteration)
            OnRemovedContinuousAlteration(continuousAlteration);
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

    /// <summary>
    /// Use this when editor serialization is not accessible.
    /// </summary>
    public void Initialize(Character character) {
        _character = character;
    }
}