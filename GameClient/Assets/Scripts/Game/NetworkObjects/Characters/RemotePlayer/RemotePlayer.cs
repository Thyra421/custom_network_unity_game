using UnityEngine;

public class RemotePlayer : Character
{
    [SerializeField]
    private RemotePlayerAnimation _animation;

    public RemotePlayerMovement Movement { get; private set; }

    protected override CharacterMovement CharacterMovement => Movement;

    protected override void Awake() {
        base.Awake();
        Movement = new RemotePlayerMovement(this);
    }

    public RemotePlayerAnimation Animation => _animation;
}