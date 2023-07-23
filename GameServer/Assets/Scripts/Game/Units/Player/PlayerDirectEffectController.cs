using System.Linq;

public class PlayerDirectEffectController : IDirectEffectController
{
    private readonly Player _player;

    public PlayerDirectEffectController(Player player) {
        _player = player;
    }

    public void Use(IUsable usable) {
        foreach (Effect effect in usable.Effects)
            typeof(PlayerDirectEffectController).GetMethod(effect.MethodName).Invoke(this, effect.Parameters.Select((EffectParameter param) => param.ToObject).ToArray());
    }

    public void AddAlteration(Alteration alteration) {
        _player.Alterations.Add(alteration);
    }

    public void RestoreHealth(int amount) {
        _player.Statistics.CurrentHealth += amount;
    }

    public void DealDamage(int amount) {
        _player.Statistics.CurrentHealth -= amount;
    }
}