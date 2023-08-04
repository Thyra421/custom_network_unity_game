using System;

[Serializable]
public class LocalPlayerAnimation : CharacterAnimation
{
    public PlayerAnimationData Data => new PlayerAnimationData(GetFloat("X"), GetFloat("Y"));
}