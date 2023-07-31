using System.Collections.Generic;
using UnityEngine;

public class NPCsManager : MonoBehaviour
{
    private readonly List<NPC> _NPCs = new List<NPC>();

    public static NPCsManager Current { get; private set; }

    public delegate void OnAddedNPCHandler(NPC NPC);
    public delegate void OnRemovedNPCHandler(NPC NPC);
    public event OnAddedNPCHandler OnAddedNPC;
    public event OnRemovedNPCHandler OnRemovedNPC;

    private NPC FindNPC(string id) => _NPCs.Find((NPC n) => n.Id == id);

    private void CreateNPC(NPCData data) {
        Animal animal = Resources.Load<Animal>($"{SharedConfig.Current.NPCsPath}/{data.animalName}");
        GameObject newObject = Instantiate(animal.Prefab, data.transformData.position.ToVector3, Quaternion.Euler(data.transformData.rotation.ToVector3));
        NPC newNPC = newObject.AddComponent<NPC>();
        newNPC.Initialize(data.id);
        _NPCs.Add(newNPC);
        OnAddedNPC?.Invoke(newNPC);
    }

    private void RemoveNPC(string id) {
        NPC NPC = FindNPC(id);
        OnRemovedNPC?.Invoke(NPC);
        _NPCs.Remove(NPC);
        Destroy(NPC.gameObject);
    }

    private void OnMessageSpawnNPCs(MessageSpawnNPCs messageSpawnNPCs) {
        foreach (NPCData n in messageSpawnNPCs.NPCs)
            CreateNPC(n);
    }

    private void OnMessageNPCsMoved(MessageNPCsMoved messageNPCsMoved) {
        foreach (NPCMovementData n in messageNPCsMoved.NPCs) {
            NPC NPC = FindNPC(n.id);

            if (NPC != null) {
                NPC.Movement.SetMovement(n.transformData, n.movementSpeed);
                NPC.Animation.SetAnimation(n.NPCAnimationData);
            }
        }
    }

    private void Awake() {
        if (Current == null)
            Current = this;
        else
            Destroy(gameObject);

        MessageHandler.Current.OnMessageSpawnNPCsEvent += OnMessageSpawnNPCs;
        MessageHandler.Current.OnMessageNPCsMovedEvent += OnMessageNPCsMoved;
    }

    public void OnMessageHealthChanged(MessageHealthChanged messageHealthChanged) {
        NPC npc = FindNPC(messageHealthChanged.character.id);

        if (npc != null) {
            npc.Health.MaxHealth = messageHealthChanged.maxHealth;
            npc.Health.CurrentHealth = messageHealthChanged.currentHealth;
        }
    }
}