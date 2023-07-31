using UnityEngine;

public class AbilitySlot
{
    private float _currentCooldown;

    public Ability CurrentAbility { get; private set; }

    public delegate void OnChangedHandler(Ability ability);
    public delegate void OnUpdatedHandler(float cooldown);
    public event OnChangedHandler OnChanged;
    public event OnUpdatedHandler OnUpdated;

    private void Melee() {
        TCPClient.Send(new MessageUseAbility(CurrentAbility.name, Vector3Data.Zero));
    }

    private void Aimed() {
        CameraController cam = CameraController.Current;
        if (cam.IsAiming) {
            TCPClient.Send(new MessageUseAbility(CurrentAbility.name, cam.AimDirection));
            cam.StopAim();
        } else
            cam.StartAim();
    }

    private void AOE(GameObject prefab) {
        GroundTargetManager targ = GroundTargetManager.Current;
        if (targ.HasTarget) {
            TCPClient.Send(new MessageUseAbility(CurrentAbility.name, targ.TargetPosition));
            targ.DestroyGroundTarget();
        } else
            targ.CreateGroundTarget(prefab);
    }

    public void Cooldown(float amount) {
        if (_currentCooldown > 0) {
            _currentCooldown = Mathf.Clamp(_currentCooldown - amount, 0, CurrentAbility.CooldownInSeconds);
            OnUpdated?.Invoke(_currentCooldown);
        }
    }

    public void Use() {
        if (CurrentAbility == null || _currentCooldown > 0)
            return;

        if (CurrentAbility is DirectAbility) {
            if (CurrentAbility is MeleeAbility)
                Melee();
            else if (CurrentAbility is AimedAbility)
                Aimed();
            else if (CurrentAbility is DirectAOEAbility directAOEAbility)
                AOE(directAOEAbility.Prefab);
        } else if (CurrentAbility is PersistentAOEAbility persistentAOEAbility)
            AOE(persistentAOEAbility.Prefab);
        else
            TCPClient.Send(new MessageUseAbility(CurrentAbility.name, Vector3Data.Zero));
    }

    public void Used() {
        _currentCooldown = CurrentAbility.CooldownInSeconds;
        OnUpdated?.Invoke(_currentCooldown);
    }

    public void Initialize(Ability ability) {
        _currentCooldown = 0;
        CurrentAbility = ability;
        OnChanged?.Invoke(CurrentAbility);
    }
}