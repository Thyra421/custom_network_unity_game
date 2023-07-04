using UnityEngine;

public class LocalPlayerAttack : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;
    private Weapon _weapon;
    private event OnWeaponEquipedHandler _onWeaponEquiped;

    public delegate void OnWeaponEquipedHandler(Weapon weapon);

    public void Equip(Weapon weapon) {
        _weapon = weapon;
        _onWeaponEquiped?.Invoke(weapon);
    }

    public void Attack() {
        _animator.SetTrigger("Attack");
        TCPClient.Send(new MessageAttack());
    }

    private void Update() {
        if (Input.GetKeyUp(KeyCode.Alpha1))
            Attack();
        if (Input.GetKeyUp(KeyCode.Alpha2))
            TCPClient.Send(new MessageUseItem("HealthPotion"));
    }

    public event OnWeaponEquipedHandler OnWeaponEquipedEvent {
        add => _onWeaponEquiped += value;
        remove => _onWeaponEquiped -= value;
    }
}
