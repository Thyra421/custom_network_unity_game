using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    [SerializeField]
    private GameObject _meleePrefab;
    [SerializeField]
    private PlayerWeaponAbilityActionController _weaponAbilityActionController;

    public void UseAbility(int index) {
        switch (index) {
            case < 3:
                _weaponAbilityActionController.Use(_weapon.Abilities[index]);
                break;
        }
    }

    public void Initialize(PlayerWeaponAbilityActionController weaponAbilityActionController) {
        _weaponAbilityActionController = weaponAbilityActionController;
    }

    private Weapon _weapon;

    public GameObject MeleePrefab => _meleePrefab;
}