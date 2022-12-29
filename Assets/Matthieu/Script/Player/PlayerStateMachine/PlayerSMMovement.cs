using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSMMovement : PlayerSM
{
    PlayerMovement _currentState;

    public PlayerMovement CurrentState { get => _currentState; }

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
    private void OnStateEnter(PlayerMovement state)
    {
        switch (state)
        {
            case PlayerMovement.IDLE:
                OnEnterIdle();
                break;

            case PlayerMovement.WALKING:
                OnEnterWalking();
                break;

            case PlayerMovement.SPRINTING:
                OnEnterSprinting();
                break;

            default:
                Debug.LogError($"OnStateEnter: Invalid state {state}");
                break;
        }

    }
    #endregion On Enter States

    // Méthode appelée à chaque frame dans un état
    #region On Update States
    private void OnStateUpdate(PlayerMovement state)
    {
        switch (state)
        {
            case PlayerMovement.IDLE:
                OnUpdateIdle();
                break;

            case PlayerMovement.WALKING:
                OnUpdateWalking();
                break;

            case PlayerMovement.SPRINTING:
                OnUpdateSprinting();
                break;

            default:
                Debug.LogError($"OnStateUpdate: Invalid state {state}");
                break;
        }

    }
    #endregion

    // Méthode appelée à chaque frame dans un état
    #region On Fixed Update States
    private void OnStateFixedUpdate(PlayerMovement state)
    {
        switch (state)
        {
            case PlayerMovement.IDLE:
                OnFixedUpdateIdle();
                break;

            case PlayerMovement.WALKING:
                OnFixedUpdateWalking();
                break;

            case PlayerMovement.SPRINTING:
                OnFixedUpdateSprinting();
                break;

            default:
                Debug.LogError($"OnStateFixedUpdate: Invalid state {state}");
                break;
        }

    }
    #endregion

    // Méthode appelée à chaque frame dans un état
    #region On Exit States
    private void OnStateExit(PlayerMovement state)
    {
        switch (state)
        {
            case PlayerMovement.IDLE:
                OnExitIdle();
                break;

            case PlayerMovement.WALKING:
                OnExitWalking();
                break;

            case PlayerMovement.SPRINTING:
                OnExitSprinting();
                break;

            default:
                Debug.LogError($"OnStateExit: Invalid state {state}");
                break;
        }

    }
    #endregion

    // Méthode appelée pour passer d'un état à un autre
    #region On Transition to States
    private void TransitionToState(PlayerMovement toState)
    {
        OnStateExit(_currentState);
        _currentState = toState;
        OnStateEnter(toState);
    }
    #endregion

    #endregion

    #region Idle State
    private void OnEnterIdle()
    {
        _anim.SetBool("Idle", true);
        _playerControls.StartIdle();
    }
    private void OnUpdateIdle()
    {
        if (_inputs.HasMovement && _healthState == PlayerHealth.AWAKE && _playerControls.IsAttackFreezeEnded && !_playerControls.GamePause && !_pauseManager.GamePaused)
            if (!_inputs.AskingRunning)
                TransitionToState(PlayerMovement.WALKING);
            else
                TransitionToState(PlayerMovement.SPRINTING);

        _playerControls.DoIdle();

        if (_healthState != PlayerHealth.AWAKE)
            _playerControls.StartIdle();
    }
    private void OnFixedUpdateIdle()
    {

    }
    private void OnExitIdle()
    {
        _anim.SetBool("Idle", false);
    }
    #endregion
    #region Walking State
    private void OnEnterWalking()
    {
        _anim.SetBool("Walk", true);

    }
    private void OnUpdateWalking()
    {
        if (_healthState != PlayerHealth.AWAKE || !_inputs.HasMovement || !_playerControls.IsAttackFreezeEnded || _playerControls.GamePause || _pauseManager.GamePaused)
                TransitionToState(PlayerMovement.IDLE);
        else if(_inputs.HasMovement && _inputs.AskingRunning)
            TransitionToState(PlayerMovement.SPRINTING);
    }
    private void OnFixedUpdateWalking()
    {
        _playerControls.DoWalk();
    }
    private void OnExitWalking()
    {
        _anim.SetBool("Walk", false);

    }
    #endregion
    #region Sprinting State
    private void OnEnterSprinting()
    {
        _anim.SetBool("Sprint", true);
    }
    private void OnUpdateSprinting()
    {
        if (_healthState != PlayerHealth.AWAKE || !_inputs.HasMovement || !_playerControls.IsAttackFreezeEnded || _playerControls.GamePause || _pauseManager.GamePaused)
            TransitionToState(PlayerMovement.IDLE);
        else if (_inputs.HasMovement && !_inputs.AskingRunning)
            TransitionToState(PlayerMovement.WALKING);
    }
    private void OnFixedUpdateSprinting()
    {
        _playerControls.DoSprint();
    }
    private void OnExitSprinting()
    {
        _anim.SetBool("Sprint", false);
    }
    #endregion
}
