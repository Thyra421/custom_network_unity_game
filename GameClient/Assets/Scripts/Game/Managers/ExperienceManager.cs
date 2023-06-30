using UnityEngine;

public class PlayerSkillExperience
{
    private float _currentRatio;
    private int _currentLevel = 1;
    private event OnChangedHandler _onChanged;

    public void SetExperience(int currentLevel, float currentRatio) {
        _currentLevel = currentLevel;
        _currentRatio = currentRatio;
        _onChanged?.Invoke(_currentRatio, _currentLevel);
    }

    public event OnChangedHandler OnChangedEvent {
        add => _onChanged += value;
        remove => _onChanged -= value;
    }

    public delegate void OnChangedHandler(float currentRatio, int currentLevel);
}

public class WeaponSkillExperience
{
    private readonly Weapon _weapon;
    private readonly PlayerSkillExperience _experience = new PlayerSkillExperience();

    public WeaponSkillExperience(Weapon weapon) {
        _weapon = weapon;
    }

    public Weapon Weapon => _weapon;

    public PlayerSkillExperience Experience => _experience;
}

public class ExperienceManager : MonoBehaviour
{
    private static ExperienceManager _current;
    private readonly PlayerSkillExperience _generalExperience = new PlayerSkillExperience();
    private readonly PlayerSkillExperience _gatheringExperience = new PlayerSkillExperience();
    private readonly PlayerSkillExperience _miningExperience = new PlayerSkillExperience();
    private readonly PlayerSkillExperience _cookingExperience = new PlayerSkillExperience();
    private readonly PlayerSkillExperience _alchemyExperience = new PlayerSkillExperience();
    private readonly PlayerSkillExperience _forgingExperience = new PlayerSkillExperience();
    private readonly PlayerSkillExperience _lumberjackingExperience = new PlayerSkillExperience();
    private readonly PlayerSkillExperience _engineeringExperience = new PlayerSkillExperience();
    private readonly PlayerSkillExperience _huntingExperience = new PlayerSkillExperience();

    //private readonly List<WeaponSkillExperience> _weaponsExperience = new List<WeaponSkillExperience>();

    private void OnExperienceChanged(MessageExperienceChanged messageExperienceChanged) {
        switch (messageExperienceChanged.type) {
            case SkillType.General:
                _generalExperience.SetExperience(messageExperienceChanged.currentLevel, messageExperienceChanged.currentRatio);
                break;
            case SkillType.Gathering:
                _gatheringExperience.SetExperience(messageExperienceChanged.currentLevel, messageExperienceChanged.currentRatio);
                break;
            case SkillType.Mining:
                _miningExperience.SetExperience(messageExperienceChanged.currentLevel, messageExperienceChanged.currentRatio);
                break;
            case SkillType.Lumberjacking:
                _lumberjackingExperience.SetExperience(messageExperienceChanged.currentLevel, messageExperienceChanged.currentRatio);
                break;
            case SkillType.Hunting:
                _huntingExperience.SetExperience(messageExperienceChanged.currentLevel, messageExperienceChanged.currentRatio);
                break;
            case SkillType.Cooking:
                _cookingExperience.SetExperience(messageExperienceChanged.currentLevel, messageExperienceChanged.currentRatio);
                break;
            case SkillType.Alchemy:
                _alchemyExperience.SetExperience(messageExperienceChanged.currentLevel, messageExperienceChanged.currentRatio);
                break;
            case SkillType.Forging:
                _forgingExperience.SetExperience(messageExperienceChanged.currentLevel, messageExperienceChanged.currentRatio);
                break;
            case SkillType.Engineering:
                _engineeringExperience.SetExperience(messageExperienceChanged.currentLevel, messageExperienceChanged.currentRatio);
                break;
        }
    }

    private void Awake() {
        if (_current == null)
            _current = this;
        else
            Destroy(gameObject);
        MessageHandler.Current.OnMessageExperienceChangedEvent += OnExperienceChanged;
    }

    public PlayerSkillExperience GeneralExperience => _generalExperience;

    public PlayerSkillExperience GatheringExperience => _gatheringExperience;

    public PlayerSkillExperience MiningExperience => _miningExperience;

    public PlayerSkillExperience CookingExperience => _cookingExperience;

    public PlayerSkillExperience AlchemyExperience => _alchemyExperience;

    public PlayerSkillExperience ForgingExperience => _forgingExperience;

    public PlayerSkillExperience LumberjackingExperience => _lumberjackingExperience;

    public PlayerSkillExperience EngineeringExperience => _engineeringExperience;

    public PlayerSkillExperience HuntingExperience => _huntingExperience;

    public static ExperienceManager Current => _current;
}
