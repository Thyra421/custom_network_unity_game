using System.Collections.Generic;
using UnityEngine;

public class Alterations : MonoBehaviour
{
    public List<AlterationController> AlterationControllers { get; } = new List<AlterationController>();

    public delegate void OnAddedHandler(AlterationController alterationController);
    public delegate void OnRemovedHandler(AlterationController alterationController);
    public event OnAddedHandler OnAdded;
    public event OnRemovedHandler OnRemoved;

    private AlterationController Find(Alteration alteration, string ownerId) => AlterationControllers.Find((AlterationController alterationController) => alterationController.Alteration == alteration && alterationController.OwnerId == ownerId);

    private void Update() {
        AlterationControllers.ForEach((AlterationController alterationController) => alterationController.Update());
    }

    public void Add(AlterationController alterationController) {
        AlterationControllers.Add(alterationController);
        OnAdded?.Invoke(alterationController);
    }

    public void Refresh(Alteration alteration, float remainingDuration, string ownerId) {
        Find(alteration, ownerId)?.Refresh(remainingDuration);
    }

    public void Remove(Alteration alteration, string ownerId) {
        AlterationController alterationController = Find(alteration, ownerId);

        if (alterationController != null) {
            AlterationControllers.Remove(alterationController);
            OnRemoved?.Invoke(alterationController);
        }
    }
}