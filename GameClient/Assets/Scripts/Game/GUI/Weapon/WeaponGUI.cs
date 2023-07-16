using UnityEngine;
using UnityEngine.UI;

public class WeaponGUI : MonoBehaviour
{
    [SerializeField]
    private Image _weaponImage;
    [SerializeField]
    private AbilitySlotGUI _ability1;
    [SerializeField]
    private AbilitySlotGUI _ability2;
    [SerializeField]
    private AbilitySlotGUI _ability3;
    [SerializeField]
    private AbilitySlotGUI _ability4;
    [SerializeField]
    private AbilitySlotGUI _ability5;

    private void OnChangedWeapon(Weapon weapon) {
        _weaponImage.sprite = weapon.Icon;
    }

    private void Awake() {
        AbilitiesManager.Current.OnChangedWeapon += OnChangedWeapon;
        _ability1.Initialize(AbilitiesManager.Current.AbilitySlot1);
        _ability2.Initialize(AbilitiesManager.Current.AbilitySlot2);
        _ability3.Initialize(AbilitiesManager.Current.AbilitySlot3);
        _ability4.Initialize(AbilitiesManager.Current.AbilitySlot4);
        _ability5.Initialize(AbilitiesManager.Current.AbilitySlot5);
    }
}