using UnityEngine;

public class ExperienceManager : MonoBehaviour
{
    public static ExperienceManager Current { get; private set; }
    public SkillExperience GeneralExperience { get; } = new SkillExperience();
    public SkillExperience GatheringExperience { get; } = new SkillExperience();
    public SkillExperience MiningExperience { get; } = new SkillExperience();
    public SkillExperience CookingExperience { get; } = new SkillExperience();
    public SkillExperience AlchemyExperience { get; } = new SkillExperience();
    public SkillExperience ForgingExperience { get; } = new SkillExperience();
    public SkillExperience LumberjackingExperience { get; } = new SkillExperience();
    public SkillExperience EngineeringExperience { get; } = new SkillExperience();
    public SkillExperience HuntingExperience { get; } = new SkillExperience();

    //private readonly List<WeaponSkillExperience> _weaponsExperience = new List<WeaponSkillExperience>();

    private void OnExperienceChanged(MessageExperienceChanged messageExperienceChanged) {
        switch (messageExperienceChanged.type) {
            case SkillType.General:
                GeneralExperience.SetExperience(messageExperienceChanged.currentLevel, messageExperienceChanged.currentRatio);
                break;
            case SkillType.Gathering:
                GatheringExperience.SetExperience(messageExperienceChanged.currentLevel, messageExperienceChanged.currentRatio);
                break;
            case SkillType.Mining:
                MiningExperience.SetExperience(messageExperienceChanged.currentLevel, messageExperienceChanged.currentRatio);
                break;
            case SkillType.Lumberjacking:
                LumberjackingExperience.SetExperience(messageExperienceChanged.currentLevel, messageExperienceChanged.currentRatio);
                break;
            case SkillType.Hunting:
                HuntingExperience.SetExperience(messageExperienceChanged.currentLevel, messageExperienceChanged.currentRatio);
                break;
            case SkillType.Cooking:
                CookingExperience.SetExperience(messageExperienceChanged.currentLevel, messageExperienceChanged.currentRatio);
                break;
            case SkillType.Alchemy:
                AlchemyExperience.SetExperience(messageExperienceChanged.currentLevel, messageExperienceChanged.currentRatio);
                break;
            case SkillType.Forging:
                ForgingExperience.SetExperience(messageExperienceChanged.currentLevel, messageExperienceChanged.currentRatio);
                break;
            case SkillType.Engineering:
                EngineeringExperience.SetExperience(messageExperienceChanged.currentLevel, messageExperienceChanged.currentRatio);
                break;
        }
    }

    private void Awake() {
        if (Current == null)
            Current = this;
        else
            Destroy(gameObject);
        MessageHandler.Current.OnMessageExperienceChangedEvent += OnExperienceChanged;
    }
}
