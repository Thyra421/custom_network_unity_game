using UnityEngine;

public struct AnimationData
{
    public float x;
    public float y;
    public bool isRunning;
    public bool isGrounded;

    public AnimationData(float x, float y, bool isRunning, bool isGrounded) {
        this.x = Mathf.Round(x * 1000) / 1000;
        this.y = Mathf.Round(y * 1000) / 1000;
        this.isRunning = isRunning;
        this.isGrounded = isGrounded;
    }

    public static AnimationData Zero => new AnimationData(0, 0, false, false);
}
