using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Environment/Waves", fileName = "Waves_")]
public class WaveScriptableObject : ScriptableObject
{
    [SerializeField] int _ennemyCount;
    [SerializeField] GameObject[] _ennemyTypes;

    public int EnnemyCount { get => _ennemyCount; }
    public GameObject[] EnnemyTypes { get => _ennemyTypes; }
}
