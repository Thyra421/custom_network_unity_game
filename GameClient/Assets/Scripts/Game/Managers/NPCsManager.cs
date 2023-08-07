using System.Collections.Generic;
using UnityEngine;

public class NPCsManager : Singleton<NPCsManager>
{
    [SerializeField]
    private GameObject _nameplatePrefab;
    private readonly List<NPC> _NPCs = new List<NPC>();

    public delegate void OnAddedNPCHandler(NPC NPC);
    public delegate void OnRemovedNPCHandler(NPC NPC);
    public event OnAddedNPCHandler OnAddedNPC;
    public event OnRemovedNPCHandler OnRemovedNPC;

    private void CreateNPC(NPCData data) {
        Animal animal = Resources.Load<Animal>($"{SharedConfig.Current.NPCsPath}/{data.animalName}");

        GameObject obj = Instantiate(animal.Prefab, data.transform.position.ToVector3, Quaternion.Euler(data.transform.rotation.ToVector3));
        NPC newNPC = obj.AddComponent<NPC>();
        newNPC.Initialize(data.id);

        GameObject nameplateObj = Instantiate(_nameplatePrefab, obj.transform);
        nameplateObj.GetComponent<HealthBarGUI>().Initialize(newNPC);
        nameplateObj.GetComponent<AlterationsGUI>().Initialize(newNPC);
        nameplateObj.GetComponent<CastBarGUI>().Initialize(newNPC);

        _NPCs.Add(newNPC);
        OnAddedNPC?.Invoke(newNPC);
    }

    private void RemoveNPC(string id) {
        NPC NPC = Find(id);
        OnRemovedNPC?.Invoke(NPC);
        _NPCs.Remove(NPC);
        Destroy(NPC.gameObject);
    }

    private void OnMessageSpawnNPCs(MessageSpawnNPCs messageSpawnNPCs) {
        foreach (NPCData n in messageSpawnNPCs.NPCs)
            CreateNPC(n);
    }

    private void OnMessageNPCsMoved(MessageNPCsMoved messageNPCsMoved) {
        foreach (UnitMovementData umd in messageNPCsMoved.NPCs) {
            NPC NPC = Find(umd.id);

            if (NPC != null)
                NPC.Movement.SetMovement(umd.transform, umd.timestamp);
        }
    }

    protected override void Awake() {
        base.Awake();

        TCPClient.MessageHandler.AddListener<MessageSpawnNPCs>(OnMessageSpawnNPCs);
        UDPClient.MessageHandler.AddListener<MessageNPCsMoved>(OnMessageNPCsMoved);
    }

    public NPC Find(string id) => _NPCs.Find((NPC n) => n.Id == id);
}