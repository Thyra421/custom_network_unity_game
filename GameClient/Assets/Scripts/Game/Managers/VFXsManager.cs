using System.Collections.Generic;
using UnityEngine;

public class VFXsManager : MonoBehaviour
{
    private readonly List<VFX> _VFXs = new List<VFX>();

    public static VFXsManager Current { get; private set; }

    private VFX FindVFX(string id) => _VFXs.Find((VFX v) => v.Id == id);

    private void CreateVFX(VFXData data) {
        GameObject obj = Resources.Load<GameObject>($"{SharedConfig.Current.PrefabsPath}/{data.prefabName}");
        GameObject newObject = Instantiate(obj, data.transformData.position.ToVector3, Quaternion.Euler(data.transformData.rotation.ToVector3));
        VFX newVFX = newObject.AddComponent<VFX>();
        newVFX.Initialize(data.id);
        _VFXs.Add(newVFX);
    }

    private void RemoveVFX(string id) {
        VFX VFX = FindVFX(id);
        _VFXs.Remove(VFX);
        Destroy(VFX.gameObject);
    }

    private void OnMessageSpawnVFX(MessageSpawnVFX messageSpawnVFXs) {
        CreateVFX(messageSpawnVFXs.VFX);
    }

    private void OnMessageDespawnVFX(MessageDespawnVFX messageDespawnVFXs) {
        RemoveVFX(messageDespawnVFXs.id);
    }

    private void OnMessageVFXMoved(MessageVFXMoved serverMessageVFXMoved) {
        foreach (VFXMovementData n in serverMessageVFXMoved.VFXs) {
            VFX VFX = FindVFX(n.id);
            if (VFX != null) {
                VFX.DestinationPosition = n.transformData.position.ToVector3;
                VFX.MovementSpeed = n.speed;
            }
        }
    }

    private void Awake() {
        if (Current == null)
            Current = this;
        else
            Destroy(gameObject);
        MessageHandler.Current.OnMessageSpawnVFXEvent += OnMessageSpawnVFX;
        MessageHandler.Current.OnMessageVFXMovedEvent += OnMessageVFXMoved;
        MessageHandler.Current.OnMessageDespawnVFXEvent += OnMessageDespawnVFX;
    }
}