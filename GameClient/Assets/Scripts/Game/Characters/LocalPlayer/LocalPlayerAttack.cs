using UnityEngine;

public class LocalPlayerAttack : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;

    public void Attack() {
       
    }

    private void Update() {
        //if (Input.GetKeyUp(KeyCode.Alpha1))
        //    Attack();
        //if (Input.GetKeyUp(KeyCode.Alpha2))
        //    TCPClient.Send(new MessageUseItem("HealthPotion"));
    }    
}
