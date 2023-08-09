using System;

public class ExperienceManager : Singleton<ExperienceManager>
{
    private readonly SkillExperience[] _skillExperiences = new SkillExperience[Enum.GetValues(typeof(ExperienceType)).Length];

    private void OnExperienceChanged(MessageExperienceChanged messageExperienceChanged) {
        foreach (ExperienceData ed in messageExperienceChanged.experiences) {
            SkillExperience skillExperience = Find(ed.type);
            skillExperience.SetExperience(ed.level, ed.ratio);
        }
    }

    protected override void Awake() {
        base.Awake();

        for (int i = 0; i < Enum.GetValues(typeof(ExperienceType)).Length; i++)
            _skillExperiences[i] = new SkillExperience((ExperienceType)Enum.GetValues(typeof(ExperienceType)).GetValue(i));

        TCPClient.MessageRegistry.AddListener<MessageExperienceChanged>(OnExperienceChanged);
    }

    public SkillExperience Find(ExperienceType experienceType) => Array.Find(_skillExperiences, (SkillExperience se) => se.ExperienceType == experienceType);

    public SkillExperience At(int index) => _skillExperiences[index];
}
