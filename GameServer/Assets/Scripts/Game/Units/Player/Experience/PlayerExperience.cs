using System;
using System.Collections.Generic;
using System.Linq;

public class PlayerExperience
{
    private readonly Player _player;
    private readonly SkillExperience[] _skillExperiences = new SkillExperience[Enum.GetValues(typeof(ExperienceType)).Length];

    private SkillExperience Find(ExperienceType experienceType) => Array.Find(_skillExperiences, (SkillExperience se) => se.ExperienceType == experienceType);

    public PlayerExperience(Player player) {
        _player = player;
        for (int i = 0; i < Enum.GetValues(typeof(ExperienceType)).Length; i++)
            _skillExperiences[i] = new SkillExperience((ExperienceType)Enum.GetValues(typeof(ExperienceType)).GetValue(i));
    }

    public void AddExperience(ExperienceType[] experienceTypes, int amount) {
        List<SkillExperience> modifiedSkills = new List<SkillExperience>();

        foreach (ExperienceType et in experienceTypes) {
            SkillExperience skillExperience = Find(et);
            skillExperience.AddExperience(amount);
            modifiedSkills.Add(skillExperience);
        }

        _player.Client.Tcp.Send(new MessageExperienceChanged(modifiedSkills.Select((SkillExperience se) => se.Data).ToArray()));
    }
}
