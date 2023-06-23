using System.Collections.Generic;
using UnityEngine;

public class PlayersGUI : MonoBehaviour
{
    [SerializeField]
    private PlayersManager _playersManager;
    [SerializeField]
    private NameplateGUI _myNameplate;
    [SerializeField]
    private GameObject _nameplateTemplate;
    private readonly List<NameplateGUI> _nameplates = new List<NameplateGUI>();

    private NameplateGUI Find(Player player) => _nameplates.Find((NameplateGUI n) => n.Player == player);

    private void OnAddedPlayer(Player player) {
        NameplateGUI nameplateGUI = Instantiate(_nameplateTemplate, player.transform).GetComponent<NameplateGUI>();
        nameplateGUI.Initialize(player);
        _nameplates.Add(nameplateGUI);
    }

    private void OnRemovedPlayer(Player player) {
        NameplateGUI nameplate = Find(player);
        _nameplates.Remove(nameplate);
        Destroy(nameplate.gameObject);
    }

    private void Start() {
        _myNameplate.Initialize(_playersManager.MyPlayer);
    }

    private void Awake() {
        _playersManager.OnAddedPlayerEvent += OnAddedPlayer;
        _playersManager.OnRemovedPlayerEvent += OnRemovedPlayer;
    }
}