using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ennemy/BasicEnnemy", fileName ="Ennemy_")]
public class EnnemyManager : ScriptableObject
{
    [Header("GLOBAL CONTROLER")]
    [Space(10)]

    [SerializeField] float _moveSpeed;
    [SerializeField] float _maxDistanceDetection;
    [SerializeField] float _maxDistanceAlignDetection;
    [SerializeField] float _attackDuration;
    [SerializeField] LayerMask _whatIsPlayer;

    [Header("IA Timer")]
    [Space(10)]
    [SerializeField] float _thinkingTime;
    [SerializeField] float _delayThinkTime;
    [SerializeField] float _invicibleTime;
    [SerializeField] float _knockbackForce;
    [SerializeField] AnimationCurve _knockbackEasing;
    [SerializeField] float _knockbackDuration;

    [Header("HEALTH CONTROLLER")]
    [Space(10)]
    [SerializeField] int _maxHealth;

    [Header("Loot Manager")]
    [Space(10)]
    [SerializeField] Rigidbody2D[] _loots;
    [Header("Quantity is the max possible ( actual quantity is random between 0 and this int ) ")]
    [SerializeField] int _lootQuantity;
    [SerializeField] int _lootDropForce;


    [Header("Projectils Manager")]
    [Space(10)]
    [SerializeField] Rigidbody2D _canBluePrefab;
    [SerializeField] float _speed;




    public float MoveSpeed { get => _moveSpeed; }
    public float ThinkingTime { get => _thinkingTime;  }
    public float InvicibleTime { get => _invicibleTime; }
    public float KnocbackForce { get => _knockbackForce; }
    public AnimationCurve KnockbackEasing { get => _knockbackEasing; }
    public int MaxHealth { get => _maxHealth; }
    public float MaxDistanceDetection { get => _maxDistanceDetection; }
    public float AttackDuration { get => _attackDuration; }
    public LayerMask WhatIsPlayer { get => _whatIsPlayer;  }
    public float DelayThinkTime { get => _delayThinkTime; }
    public float KnockbackDuration { get => _knockbackDuration; }
    public Rigidbody2D[] Loots { get => _loots; }
    public int LootQuantity { get => _lootQuantity; }
    public int LootDropForce { get => _lootDropForce; }
    public float MaxDistanceAlignDetection { get => _maxDistanceAlignDetection;}
    public Rigidbody2D CanBluePrefab { get => _canBluePrefab;}
    public float Speed { get => _speed;}
}
