public class VFXMovement
{
    private float _speed;

    public VFX VFX { get; }
    public VFXMovementData Data => new VFXMovementData(VFX.Id, new TransformData(VFX.transform), _speed);

    public VFXMovement(VFX vfx) {
        VFX = vfx;
    }

    public void Initialize(float speed) {
        _speed = speed;
    }
}