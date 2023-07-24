using System.Linq;

public class PlayerDirectEffectController : IDirectEffectController
{
    private readonly Player _target;
    private readonly Player _owner;

    public PlayerDirectEffectController(Player target, Player owner) {
        _target = target;
        _owner = owner;
    }

    public void Use(IUsable usable) {
        foreach (Effect effect in usable.Effects)
            typeof(PlayerDirectEffectController).GetMethod(effect.MethodName).Invoke(this, effect.Parameters.Select((EffectParameter param) => param.ToObject).ToArray());
    }

    public void AddAlteration(Alteration alteration) {
        _target.Alterations.Add(alteration, _owner);
    }

    public void RestoreHealth(int amount) {
        _target.Statistics.CurrentHealth += amount;
    }

    public void DealDamage(int amount) {
        _target.Statistics.CurrentHealth -= amount;
    }
}