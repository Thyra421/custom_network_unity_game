﻿using UnityEngine;

[CreateAssetMenu]
public class AimedAbility : OffensiveAbility
{
    [SerializeField]
    private GameObject _prefab;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _distance;

    public GameObject Prefab => _prefab;

    public float Speed => _speed;

    public float Distance => _distance;
}