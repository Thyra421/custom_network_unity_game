using UnityEngine;

public struct AnimationData
{
    public float x;
    public float y;

    public AnimationData(float x, float y) {
        this.x = Mathf.Round(x * 1000) / 1000;
        this.y = Mathf.Round(y * 1000) / 1000;
    }

    public static AnimationData Zero => new AnimationData(0, 0);
}
