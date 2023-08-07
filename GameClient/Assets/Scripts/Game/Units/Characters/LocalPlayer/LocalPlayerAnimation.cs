using System;

[Serializable]
public class LocalPlayerAnimation : CharacterAnimation
{
    public PlayerAnimationData Data => new PlayerAnimationData(GetFloat("X"), GetFloat("Y"));

    public override void SetBool(string boolName, bool value) {
        base.SetBool(boolName, value);
    }

    public override void SetFloat(string floatName, float value) {
        base.SetFloat(floatName, value);
    }

    public override void SetTrigger(string triggerName) {
        base.SetTrigger(triggerName);
    }
}