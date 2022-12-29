using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/Manager", fileName = "PlayerManager")]
public class PlayerManager : ScriptableObject
{
    #region Health / Life Manager
    [Header("HEALTH / LIFE MANAGER")]
    [Space(10)]
    [SerializeField] private int _maxHealth = 10;
    [SerializeField] private int _currentHealth;
    [SerializeField] private int _maxLifeCount = 5;
    [SerializeField] private int _currentLifeCount;

    public int MaxHealth { get => _maxHealth; }
    public int CurrentHealth { get => _currentHealth; set => _currentHealth = value; }
    public int MaxLifeCount { get => _maxLifeCount; }
    public int CurrentLifeCount { get => _currentLifeCount; set => _currentLifeCount = value; }
    #endregion

    #region Timer Manager
    [Header("TIME MANAGER")]
    [Space(10)]
    [SerializeField] private float _respawnTime;
    [SerializeField] private float _recoverTime;
    [SerializeField] private float _comboTime;
    [SerializeField] private float _attackTime;

    public float RespawnTime { get => _respawnTime; }
    public float RecoverTime { get => _recoverTime; }
    public float ComboTime { get => _comboTime; }
    public float AttackTime { get => _attackTime; }
    #endregion

    #region Hurt Manager
    [Header("HURT MANAGER")]
    [Space(10)]
    [SerializeField] private float _invicibleDuration;
    [SerializeField] private float _knockbackForce;
    [SerializeField] private float _knockbackDuration;
    [SerializeField] private AnimationCurve _knockbackEasing; 

    public float InvicibleDuration { get => _invicibleDuration; }
    public float KnockbackForce { get => _knockbackForce; }
    public float KnockbackDuration { get => _knockbackDuration; }
    public AnimationCurve KnockbackEasing { get => _knockbackEasing; }
    #endregion

    #region Score Manager
    [Header("SCORE MANAGER")]
    [Space(10)]
    [SerializeField] private int _score;
    
    public int Score { get => _score; set => _score = value; }
    #endregion
}
