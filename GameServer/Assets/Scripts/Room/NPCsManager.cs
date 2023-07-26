using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;
using System.Collections;

public class NPCsManager : MonoBehaviour
{
    [SerializeField]
    private Room _room;
    private readonly List<NPC> _NPCs = new List<NPC>();
    private float _elapsedTime = 0f;

    private void SyncMovement() {
        _elapsedTime += Time.deltaTime;
        if (_elapsedTime >= (1f / SharedConfig.SYNC_FREQUENCY)) {
            _elapsedTime = 0f;

            NPCMovementData[] NPCDatas = GetNPCMovementDatas((NPC n) => n.UpdateTransformIfChanged());
            if (NPCDatas.Length > 0)
                _room.PlayersManager.BroadcastUDP(new MessageNPCMoved(NPCDatas));
        }
    }

    private NPCData[] GetNPCDatas(Predicate<NPC> condition) =>
        _NPCs.FindAll(condition).Select((NPC npc) => npc.Data).ToArray();

    private NPCMovementData[] GetNPCMovementDatas(Predicate<NPC> condition) =>
        _NPCs.FindAll(condition).Select((NPC npc) => npc.MovementData).ToArray();

    private IEnumerator Respawn(NPCArea area) {
        yield return new WaitForSeconds(area.Animal.RespawnTimerInSeconds);
        NPC newNPC = CreateNPC(area);
        _room.PlayersManager.BroadcastTCP(new MessageSpawnNPCs(new NPCData[] { newNPC.Data }));
    }

    private void PrepareArea(NPCArea area) {
        for (int i = 0; i < area.Amount; i++)
            CreateNPC(area);
    }

    private void SpawnNPCs() {
        foreach (NPCArea area in GameManager.Current.NPCAreas)
            PrepareArea(area);
    }

    private void Awake() {
        SpawnNPCs();
    }

    private void Update() {
        SyncMovement();
    }

    public NPC CreateNPC(NPCArea area) {
        GameObject newGameObject = Instantiate(area.Animal.Prefab, area.RandomPosition, Quaternion.identity, transform);
        NPC newNPC = newGameObject.AddComponent<NPC>();
        newNPC.Initialize(area);
        newGameObject.name = $"{area.Animal.DisplayName} {newNPC.Id}";
        _NPCs.Add(newNPC);
        Debug.Log($"[NPCs] created => {_NPCs.Count} NPCs");
        return newNPC;
    }

    public void RemoveNPC(NPC npc) {
        _NPCs.Remove(npc);
        Destroy(npc.gameObject);
        Debug.Log($"[NPCs] removed => {_NPCs.Count} NPCs");
    }

    public NPCData[] NPCDatas => _NPCs.Select((NPC npc) => npc.Data).ToArray();

    public NPCMovementData[] NPCMovementDatas => _NPCs.Select((NPC npc) => npc.MovementData).ToArray();
}