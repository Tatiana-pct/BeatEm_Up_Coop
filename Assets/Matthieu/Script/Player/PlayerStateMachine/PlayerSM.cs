using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSM : MonoBehaviour
{
    protected PlayerControls _playerControls;
    protected PlayerInputs _inputs;
    protected Animator _anim;
    protected Transform _transform;

    protected PlayerHealth _healthState;
    [SerializeField] protected PauseManager _pauseManager;

    virtual protected void Awake()
    {
        _inputs = GetComponent<PlayerInputs>();
        _healthState = GetComponent<PlayerSMHealth>().CurrentState;
        _transform = transform;
        _anim = GetComponentInChildren<Animator>();
        _playerControls = GetComponent<PlayerControls>();
    }
}
