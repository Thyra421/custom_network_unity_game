public class PlayerAnimation
{
    public PlayerAnimationData Data { get; private set; }

    public void SetAnimation(PlayerAnimationData data) {
        Data = data;
    }
}