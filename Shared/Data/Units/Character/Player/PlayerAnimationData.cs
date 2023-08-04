using UnityEngine;

public struct PlayerAnimationData
{
    public float x;
    public float y;

    public PlayerAnimationData(float x, float y) {
        this.x = Mathf.Round(x * 1000) / 1000;
        this.y = Mathf.Round(y * 1000) / 1000;        
    }
}
