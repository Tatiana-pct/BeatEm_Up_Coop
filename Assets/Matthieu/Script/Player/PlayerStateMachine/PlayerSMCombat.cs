using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSMCombat : PlayerSM
{
    PlayerCombat _currentState;
    PlayerJump _jumpState;

    protected override void Awake()
    {
        base.Awake();
        _jumpState = GetComponent<PlayerSMJump>().CurrentState;
    }
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
    private void OnStateEnter(PlayerCombat state)
    {
        switch (state)
        {
            case PlayerCombat.PEACEFUL:
                OnEnterPeaceful();
                break;

            case PlayerCombat.ATTACKING:
                OnEnterAttacking();
                break;

            default:
                Debug.LogError($"OnStateEnter: Invalid state {state}");
                break;
        }

    }
    #endregion On Enter States

    // Méthode appelée à chaque frame dans un état
    #region On Update States
    private void OnStateUpdate(PlayerCombat state)
    {
        switch (state)
        {
            case PlayerCombat.PEACEFUL:
                OnUpdatePeaceful();
                break;

            case PlayerCombat.ATTACKING:
                OnUpdateAttacking();
                break;

            default:
                Debug.LogError($"OnStateUpdate: Invalid state {state}");
                break;
        }

    }
    #endregion

    // Méthode appelée à chaque frame dans un état
    #region On Fixed Update States
    private void OnStateFixedUpdate(PlayerCombat state)
    {
        switch (state)
        {
            case PlayerCombat.PEACEFUL:
                OnFixedUpdatePeaceful();
                break;

            case PlayerCombat.ATTACKING:
                OnFixedUpdateAttacking();
                break;

            default:
                Debug.LogError($"OnStateFixedUpdate: Invalid state {state}");
                break;
        }

    }
    #endregion

    // Méthode appelée à chaque frame dans un état
    #region On Exit States
    private void OnStateExit(PlayerCombat state)
    {
        switch (state)
        {
            case PlayerCombat.PEACEFUL:
                OnExitPeaceful();
                break;

            case PlayerCombat.ATTACKING:
                OnExitAttacking();
                break;

            default:
                Debug.LogError($"OnStateExit: Invalid state {state}");
                break;
        }

    }
    #endregion

    // Méthode appelée pour passer d'un état à un autre
    #region On Transition to States
    private void TransitionToState(PlayerCombat toState)
    {
        OnStateExit(_currentState);
        _currentState = toState;
        OnStateEnter(toState);
    }
    #endregion

    #endregion

    #region Peaceful State
    private void OnEnterPeaceful()
    {

    }
    private void OnUpdatePeaceful()
    {
        if (_healthState == PlayerHealth.AWAKE && _inputs.AskingAttack && !_playerControls.GamePause && !_pauseManager.GamePaused)
            TransitionToState(PlayerCombat.ATTACKING);
    }
    private void OnFixedUpdatePeaceful()
    {

    }
    private void OnExitPeaceful()
    {

    }
    #endregion
    #region Attacking State
    private void OnEnterAttacking()
    {
        _anim.SetBool("Attack", true);
        _anim.SetFloat("RandomAttack", Random.Range(0, 4));
        _playerControls.StartAttack();
        _playerControls.DoAttack();
    }
    private void OnUpdateAttacking()
    {
        if (_playerControls.IsAttackFreezeEnded || _healthState != PlayerHealth.AWAKE || _jumpState == PlayerJump.LANDING || _playerControls.GamePause || _pauseManager.GamePaused)
            TransitionToState(PlayerCombat.PEACEFUL);

    }
    private void OnFixedUpdateAttacking()
    {

    }
    private void OnExitAttacking()
    {
        _anim.SetBool("Attack", false);
    }
    #endregion
}
