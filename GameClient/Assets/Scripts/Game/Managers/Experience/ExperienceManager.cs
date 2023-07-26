using System;
using UnityEngine;

public class ExperienceManager : MonoBehaviour
{
    private readonly SkillExperience[] _skillExperiences = new SkillExperience[Enum.GetValues(typeof(ExperienceType)).Length];

    public static ExperienceManager Current { get; private set; }

    private void OnExperienceChanged(MessageExperienceChanged messageExperienceChanged) {
        foreach (ExperienceData ed in messageExperienceChanged.experiences) {
            SkillExperience skillExperience = Find(ed.type);
            skillExperience.SetExperience(ed.level, ed.ratio);
        }
    }

    private void Awake() {
        if (Current == null)
            Current = this;
        else
            Destroy(gameObject);
        for (int i = 0; i < Enum.GetValues(typeof(ExperienceType)).Length; i++)
            _skillExperiences[i] = new SkillExperience((ExperienceType)Enum.GetValues(typeof(ExperienceType)).GetValue(i));
        MessageHandler.Current.OnMessageExperienceChangedEvent += OnExperienceChanged;
    }

    public SkillExperience Find(ExperienceType experienceType) => Array.Find(_skillExperiences, (SkillExperience se) => se.ExperienceType == experienceType);

    public SkillExperience At(int index) => _skillExperiences[index];
}
