using System.Collections.Generic;
using UnityEngine;

public class AlterationsGUI : MonoBehaviour
{
    [SerializeField]
    private Transform _parent;
    [SerializeField]
    private GameObject _prefab;
    [SerializeField]
    private Alterations _alterations;
    private readonly List<AlterationGUI> _alterationGUIs = new List<AlterationGUI>();

    private AlterationGUI Find(AlterationController alterationController) => _alterationGUIs.Find((AlterationGUI alterationGUI) => alterationGUI.AlterationController == alterationController);

    private void OnAdded(AlterationController alterationController) {
        AlterationGUI newAlterationGUI = Instantiate(_prefab, _parent).GetComponent<AlterationGUI>();

        newAlterationGUI.Initialize(alterationController);
        _alterationGUIs.Add(newAlterationGUI);
    }

    private void OnRemoved(AlterationController alterationController) {
        AlterationGUI alterationGUI = Find(alterationController);

        if (alterationGUI != null) {
            _alterationGUIs.Remove(alterationGUI);
            Destroy(alterationGUI.gameObject);
        }
    }

    private void Awake() {
        _alterations.OnAdded += OnAdded;
        _alterations.OnRemoved += OnRemoved;
    }
}