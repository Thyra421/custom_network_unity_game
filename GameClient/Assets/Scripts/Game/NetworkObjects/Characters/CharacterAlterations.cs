using System.Collections.Generic;

public class CharacterAlterations
{
    private readonly List<AlterationController> _alterationControllers = new List<AlterationController>();

    public delegate void OnAddedHandler(AlterationController alterationController);
    public delegate void OnRemovedHandler(AlterationController alterationController);
    public event OnAddedHandler OnAdded;
    public event OnRemovedHandler OnRemoved;

    private AlterationController Find(Alteration alteration, string ownerId) => _alterationControllers.Find((AlterationController alterationController) => alterationController.Alteration == alteration && alterationController.OwnerId == ownerId);    

    public void Add(AlterationController alterationController) {
        _alterationControllers.Add(alterationController);
        OnAdded?.Invoke(alterationController);
    }

    public void Refresh(Alteration alteration, float remainingDuration, string ownerId) {
        Find(alteration, ownerId)?.Refresh(remainingDuration);
    }

    public void Remove(Alteration alteration, string ownerId) {
        AlterationController alterationController = Find(alteration, ownerId);

        if (alterationController != null) {
            _alterationControllers.Remove(alterationController);
            OnRemoved?.Invoke(alterationController);
        }
    }

    public void Update() {
        _alterationControllers.ForEach((AlterationController alterationController) => alterationController.Update());
    }
}