using UnityEngine;

public class ExperienceManager : MonoBehaviour
{
    public static ExperienceManager Current { get; private set; }
    public PlayerSkillExperience GeneralExperience { get; } = new PlayerSkillExperience();
    public PlayerSkillExperience GatheringExperience { get; } = new PlayerSkillExperience();
    public PlayerSkillExperience MiningExperience { get; } = new PlayerSkillExperience();
    public PlayerSkillExperience CookingExperience { get; } = new PlayerSkillExperience();
    public PlayerSkillExperience AlchemyExperience { get; } = new PlayerSkillExperience();
    public PlayerSkillExperience ForgingExperience { get; } = new PlayerSkillExperience();
    public PlayerSkillExperience LumberjackingExperience { get; } = new PlayerSkillExperience();
    public PlayerSkillExperience EngineeringExperience { get; } = new PlayerSkillExperience();
    public PlayerSkillExperience HuntingExperience { get; } = new PlayerSkillExperience();

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
