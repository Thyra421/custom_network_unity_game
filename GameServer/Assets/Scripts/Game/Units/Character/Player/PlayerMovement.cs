using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Player _player;
    private float _elapsedTime;

    private float MovementSpeed => _player.Statistics.Find(StatisticType.MovementSpeed).AlteredValue * SharedConfig.Current.PlayerMovementSpeed;
    public PlayerMovementData Data => new PlayerMovementData(_player.Id, new TransformData(transform), _player.Animation.Data, MovementSpeed);
    public bool IsMoving => _elapsedTime <= .05f;
    public bool CanMove => !_player.States.Find(StateType.Rooted).Value;

    private void Update() {
        _elapsedTime += Time.deltaTime;
    }

    public float Distance(Vector3Data otherPosition) {
        return new Vector3Data(transform.position).Distance(otherPosition);
    }

    public void SetMovement(TransformData transformData) {
        if (!transformData.position.Equals(new Vector3Data(transform.position)))
            _elapsedTime = 0;
        transform.position = transformData.position.ToVector3;
        transform.eulerAngles = transformData.rotation.ToVector3;
    }
}