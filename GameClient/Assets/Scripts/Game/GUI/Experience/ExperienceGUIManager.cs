using System;
using UnityEngine;

public class ExperienceGUIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _prefab;
    [SerializeField]
    private Transform _parent;

    private readonly SkillExperienceGUI[] _skillExperiences = new SkillExperienceGUI[Enum.GetValues(typeof(ExperienceType)).Length];

    private void Start() {
        for (int i = 0; i < Enum.GetValues(typeof(ExperienceType)).Length; i++) {
            SkillExperienceGUI newExperienceGUI = Instantiate(_prefab, _parent).GetComponent<SkillExperienceGUI>();
            newExperienceGUI.Initialize(ExperienceManager.Current.At(i));
            _skillExperiences[i] = newExperienceGUI;
        }
    }
}