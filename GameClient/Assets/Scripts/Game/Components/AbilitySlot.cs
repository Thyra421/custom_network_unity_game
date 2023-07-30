using UnityEngine;

public class AbilitySlot
{
    private float _currentCooldown;

    public Ability CurrentAbility { get; private set; }

    public delegate void OnChangedHandler(Ability ability);
    public delegate void OnUpdatedHandler(float cooldown);
    public event OnChangedHandler OnChanged;
    public event OnUpdatedHandler OnUpdated;

    public void Cooldown(float amount) {
        if (_currentCooldown > 0) {
            _currentCooldown = Mathf.Clamp(_currentCooldown - amount, 0, CurrentAbility.Cooldown);
            OnUpdated?.Invoke(_currentCooldown);
        }
    }

    public void Use() {
        if (CurrentAbility == null || _currentCooldown > 0)
            return;
        if (CurrentAbility is OffensiveAbility) {
            if (CurrentAbility is MeleeAbility)
                TCPClient.Send(new MessageUseAbility(CurrentAbility.name, Vector3Data.Zero));
            else if (CurrentAbility is AimedAbility) {
                CameraController cam = CameraController.Current;
                if (cam.IsAiming) {
                    TCPClient.Send(new MessageUseAbility(CurrentAbility.name, cam.AimDirection));
                    cam.StopAim();
                } else
                    cam.StartAim();
            }
            else if (CurrentAbility is AOEAbility) {
                GroundTargetManager targ = GroundTargetManager.Current;
                if (targ.HasTarget) {
                    TCPClient.Send(new MessageUseAbility(CurrentAbility.name, targ.TargetPosition));
                    targ.DestroyGroundTarget();
                } else
                    targ.CreateGroundTarget();
            }
        } else
            TCPClient.Send(new MessageUseAbility(CurrentAbility.name, Vector3Data.Zero));
    }

    public void Used() {
        _currentCooldown = CurrentAbility.Cooldown;
        OnUpdated?.Invoke(_currentCooldown);
    }

    public void Initialize(Ability ability) {
        _currentCooldown = 0;
        CurrentAbility = ability;
        OnChanged?.Invoke(CurrentAbility);
    }
}