using UnityEngine;

public class AlterationController
{
    public Alteration Alteration { get; }
    public Player Owner { get;  }
    public float RemainingDuration { get; private set; }

    public AlterationController(Alteration alteration, Player owner) {
        Alteration = alteration;
        Owner = owner;
        RemainingDuration = alteration.BaseDuration;
    }

    public virtual void Update() {
        RemainingDuration -= Time.deltaTime;
    }
}
