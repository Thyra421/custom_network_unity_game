using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;

public class VFXsManager : MonoBehaviour
{
    [SerializeField]
    private Room _room;
    private readonly List<VFX> _VFXs = new List<VFX>();
    private float _elapsedTime = 0f;

    private void SyncMovement() {
        _elapsedTime += Time.deltaTime;
        if (_elapsedTime >= (1f / SharedConfig.SYNC_FREQUENCY)) {
            _elapsedTime = 0f;

            VFXData[] VFXDatas = GetVFXDatas((VFX v) => v.UpdateTransformIfChanged());
            if (VFXDatas.Length > 0)
                _room.PlayersManager.BroadcastUDP(new MessageVFXMoved(VFXDatas));
        }
    }

    private VFXData[] GetVFXDatas(Predicate<VFX> condition) =>
        _VFXs.FindAll(condition).Select((VFX vfx) => vfx.Data).ToArray();

    private void Update() {
        SyncMovement();
    }

    public VFX CreateVFX(GameObject obj, string prefabName) {
        VFX newVFX = obj.AddComponent<VFX>();
        newVFX.Initialize(this, prefabName);
        obj.name = $"{prefabName} {newVFX.Id}";
        _VFXs.Add(newVFX);
        Debug.Log($"[VFXs] created => {_VFXs.Count} VFXs");
        _room.PlayersManager.BroadcastTCP(new MessageSpawnVFX(newVFX.Data));
        return newVFX;
    }

    public void RemoveVFX(VFX vfx) {
        _room.PlayersManager.BroadcastTCP(new MessageDespawnVFX(vfx.Id));
        _VFXs.Remove(vfx);
        // destroy called by AttackHitbox
        Debug.Log($"[VFXs] removed => {_VFXs.Count} VFXs");
    }

    public VFXData[] VFXDatas => _VFXs.Select((VFX vfx) => vfx.Data).ToArray();
}