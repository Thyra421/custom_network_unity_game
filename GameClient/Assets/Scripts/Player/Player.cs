using UnityEngine;

[RequireComponent(typeof(PlayerHealth))]
public abstract class Player : NetworkObject
{
    [SerializeField]
    private PlayerHealth health;

    public PlayerHealth Health {
        get => health;
        set => health = value;
    }
}
