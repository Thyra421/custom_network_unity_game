using System.Collections.Generic;
using UnityEngine;

public class VFXsManager : Singleton<VFXsManager>
{
    private readonly List<VFX> _VFXs = new List<VFX>();

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

    private void OnMessageVFXsMoved(MessageVFXsMoved serverMessageVFXsMoved) {
        foreach (UnitMovementData n in serverMessageVFXsMoved.VFXs) {
            VFX VFX = FindVFX(n.id);

            if (VFX != null)
                VFX.Movement.SetMovement(n.transform, n.timestamp);
        }
    }

    protected override void Awake() {
        base.Awake();

        UDPClient.MessageHandler.AddListener<MessageVFXsMoved>(OnMessageVFXsMoved);
        TCPClient.MessageHandler.AddListener<MessageSpawnVFX>(OnMessageSpawnVFX);
        TCPClient.MessageHandler.AddListener<MessageDespawnVFX>(OnMessageDespawnVFX);
    }
}