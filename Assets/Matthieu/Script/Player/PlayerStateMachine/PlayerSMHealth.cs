using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSMHealth : PlayerSM
{
    // SO use for life / health manager
    [SerializeField] PlayerManager _playerManager;

    PlayerHealth _currentState;

    public PlayerHealth CurrentState { get => _currentState; }
    public bool HitBoxTriggered { get; set; }
    public GameObject EnnemyGO { get; set; }

    private void Update()
    {
        OnStateUpdate(_currentState);
    }
    private void FixedUpdate()
    {
        OnStateFixedUpdate(_currentState);
    }

    // State Machine Controller
    #region Main State pattern

    // Méthode appelée lorsque l'on entre dans un état
    #region On Enter States
    // PlayerCombat state
    private void OnStateEnter(PlayerHealth state)
    {
        switch (state)
        {
            case PlayerHealth.AWAKE:
                OnEnterAwake();
                break;

            case PlayerHealth.HURT:
                OnEnterHurt();
                break;

            case PlayerHealth.RESURECTING:
                OnEnterResurecting();
                break;

            case PlayerHealth.DEAD:
                OnEnterDead();
                break;

            default:
                Debug.LogError($"OnStateEnter: Invalid state {state}");
                break;
        }

    }
    #endregion On Enter States

    // Méthode appelée à chaque frame dans un état
    #region On Update States
    private void OnStateUpdate(PlayerHealth state)
    {
        switch (state)
        {
            case PlayerHealth.AWAKE:
                OnUpdateAwake();
                break;

            case PlayerHealth.HURT:
                OnUpdateHurt();
                break;

            case PlayerHealth.RESURECTING:
                OnUpdateResurecting();
                break;

            case PlayerHealth.DEAD:
                OnUpdateDead();
                break;

            default:
                Debug.LogError($"OnStateUpdate: Invalid state {state}");
                break;
        }

    }
    #endregion

    // Méthode appelée à chaque frame dans un état
    #region On Fixed Update States
    private void OnStateFixedUpdate(PlayerHealth state)
    {
        switch (state)
        {
            case PlayerHealth.AWAKE:
                OnFixedUpdateAwake();
                break;

            case PlayerHealth.HURT:
                OnFixedUpdateHurt();
                break;

            case PlayerHealth.RESURECTING:
                OnFixedUpdateResurecting();
                break;

            case PlayerHealth.DEAD:
                OnFixedUpdateDead();
                break;

            default:
                Debug.LogError($"OnStateFixedUpdate: Invalid state {state}");
                break;
        }

    }
    #endregion

    // Méthode appelée à chaque frame dans un état
    #region On Exit States
    private void OnStateExit(PlayerHealth state)
    {
        switch (state)
        {
            case PlayerHealth.AWAKE:
                OnExitAwake();
                break;

            case PlayerHealth.HURT:
                OnExitHurt();
                break;

            case PlayerHealth.RESURECTING:
                OnExitResurecting();
                break;

            case PlayerHealth.DEAD:
                OnExitDead();
                break;

            default:
                Debug.LogError($"OnStateExit: Invalid state {state}");
                break;
        }

    }
    #endregion

    // Méthode appelée pour passer d'un état à un autre
    #region On Transition to States
    private void TransitionToState(PlayerHealth toState)
    {
        OnStateExit(_currentState);
        _currentState = toState;
        OnStateEnter(toState);
    }
    #endregion

    #endregion

    #region Awake State
    private void OnEnterAwake()
    {

    }
    private void OnUpdateAwake()
    {
        if (!_playerControls.GamePause && HitBoxTriggered && _playerControls.IsInvicibleEnded && _pauseManager.GamePaused)
            TransitionToState(PlayerHealth.HURT);
    }
    private void OnFixedUpdateAwake()
    {

    }
    private void OnExitAwake()
    {

    }
    #endregion
    #region Hurt State
    private void OnEnterHurt()
    {
        _anim.SetBool("Hurt", true);
        HitBoxTriggered = false;
        _playerControls.StartHurt();
    }
    private void OnUpdateHurt()
    {
        if (_playerControls.IsKnockBackEnded && !_pauseManager.GamePaused)
            if (_playerManager.CurrentHealth > 0)
                TransitionToState(PlayerHealth.AWAKE);
            else
            {
                if (_playerManager.CurrentLifeCount <= 1)
                    TransitionToState(PlayerHealth.DEAD);
                else
                    TransitionToState(PlayerHealth.RESURECTING);
            }
    }
    private void OnFixedUpdateHurt()
    {
        _playerControls.DoHurt(EnnemyGO);
    }
    private void OnExitHurt()
    {
        _anim.SetBool("Hurt", false);
    }
    #endregion
    #region Resurecting State
    private void OnEnterResurecting()
    {
        _anim.SetTrigger("Death");
        _playerControls.StartResurrect();
    }
    private void OnUpdateResurecting()
    {
        _playerControls.DoResurect();

        if(_playerControls.IsRespawnEnded && !_pauseManager.GamePaused)
            TransitionToState(PlayerHealth.AWAKE);
    }
    private void OnFixedUpdateResurecting()
    {

    }
    private void OnExitResurecting()
    {

    }
    #endregion
    #region Dead State
    private void OnEnterDead()
    {
        _anim.SetTrigger("Death");
        _anim.SetBool("Dead", true);
        _playerControls.DoDead();
        // Game Over Scene load
    }
    private void OnUpdateDead()
    {


    }
    private void OnFixedUpdateDead()
    {

    }
    private void OnExitDead()
    {

    }
    #endregion
}
