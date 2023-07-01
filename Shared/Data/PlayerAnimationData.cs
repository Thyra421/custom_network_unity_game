using UnityEngine;

public struct PlayerAnimationData
{
    public float x;
    public float y;
    public bool isRunning;
    public bool isGrounded;

    public PlayerAnimationData(float x, float y, bool isRunning, bool isGrounded) {
        this.x = Mathf.Round(x * 1000) / 1000;
        this.y = Mathf.Round(y * 1000) / 1000;
        this.isRunning = isRunning;
        this.isGrounded = isGrounded;
    }
}
