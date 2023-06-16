public class MovementData
{
    public float x;
    public float y;

    public MovementData() {
    }

    public MovementData(float x, float y) {
        this.x = x;
        this.y = y;
    }

    public static MovementData Zero() => new MovementData(0, 0);
}