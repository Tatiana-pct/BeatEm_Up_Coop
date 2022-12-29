using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Environment/Collectibles", fileName = "Collectibles_")]
public class CollectiblesManager : ScriptableObject
{
    [SerializeField] private string _collectibleName;
    [Space(10)]
    [SerializeField] private int _scoreValue;
    [SerializeField] private int _healthValue;

    public int ScoreValue { get => _scoreValue; }
    public int HealthValue { get => _healthValue; }
    public string CollectibleName { get => _collectibleName; }
}
