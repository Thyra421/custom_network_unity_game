using UnityEngine;

public class ExperienceGUI : MonoBehaviour
{
    [SerializeField]
    private SkillExperienceGUI _generalExperienceGUI;
    [SerializeField]
    private SkillExperienceGUI _gatheringExperienceGUI;
    [SerializeField]
    private SkillExperienceGUI _miningExperienceGUI;
    [SerializeField]
    private SkillExperienceGUI _cookingExperienceGUI;
    [SerializeField]
    private SkillExperienceGUI _alchemyExperienceGUI;
    [SerializeField]
    private SkillExperienceGUI _forgingExperienceGUI;
    [SerializeField]
    private SkillExperienceGUI _lumberjackingExperienceGUI;
    [SerializeField]
    private SkillExperienceGUI _engineeringExperienceGUI;
    [SerializeField]
    private SkillExperienceGUI _huntingExperienceGUI;

    private void Awake() {
        _generalExperienceGUI.Initialize("Character", ExperienceManager.Current.GeneralExperience);
        _gatheringExperienceGUI.Initialize("Gathering", ExperienceManager.Current.GatheringExperience);
        _miningExperienceGUI.Initialize("Mining", ExperienceManager.Current.MiningExperience);
        _cookingExperienceGUI.Initialize("Cooking", ExperienceManager.Current.CookingExperience);
        _alchemyExperienceGUI.Initialize("Alchemy", ExperienceManager.Current.AlchemyExperience);
        _forgingExperienceGUI.Initialize("Forging", ExperienceManager.Current.ForgingExperience);
        _lumberjackingExperienceGUI.Initialize("Lumberjacking", ExperienceManager.Current.LumberjackingExperience);
        _engineeringExperienceGUI.Initialize("Engineering", ExperienceManager.Current.EngineeringExperience);
        _huntingExperienceGUI.Initialize("Hunting", ExperienceManager.Current.HuntingExperience);
    }
}