public class VFX : Unit
{
    private string _prefabName;
    private VFXsManager _VFXsManager;

    public VFXMovement Movement { get; set; }
    public VFXData Data => new VFXData(Id, new TransformData(transform), _prefabName);

    private void Awake() {
        Movement = new VFXMovement(this);
    }

    private void OnDestroy() {
        _VFXsManager.RemoveVFX(this);
    }

    public void Initialize(VFXsManager VFXsManager, string prefabName, float speed) {
        _VFXsManager = VFXsManager;
        _prefabName = prefabName;
        Movement.Initialize(speed);
    }
}