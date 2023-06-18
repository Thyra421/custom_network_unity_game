using UnityEngine;

public class MovementData
{
    public float x;
    public float y;

    public MovementData() {
    }

    public MovementData(float x, float y) {
        this.x  = Mathf.Round(x * 1000) / 1000;
        this.y = Mathf.Round(y * 1000) / 1000;
    }

    public static MovementData Zero() => new MovementData(0,0);
}
