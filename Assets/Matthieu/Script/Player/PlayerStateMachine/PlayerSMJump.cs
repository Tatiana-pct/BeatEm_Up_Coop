using UnityEngine;

public class PlayerSMJump : PlayerSM
{
    PlayerJump _currentState;

    public PlayerJump CurrentState { get => _currentState; }

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
    private void OnStateEnter(PlayerJump state)
    {
        switch (state)
        {
            case PlayerJump.GROUNDED:
                OnEnterGrounded();
                break;

            case PlayerJump.JUMPING:
                OnEnterJumping();
                break;

            case PlayerJump.LANDING:
                OnEnterLanding();
                break;

            default:
                Debug.LogError($"OnStateEnter: Invalid state {state}");
                break;
        }

    }
    #endregion On Enter States

    // Méthode appelée à chaque frame dans un état
    #region On Update States
    private void OnStateUpdate(PlayerJump state)
    {
        switch (state)
        {
            case PlayerJump.GROUNDED:
                OnUpdateGrounded();
                break;

            case PlayerJump.JUMPING:
                OnUpdateJumping();
                break;

            case PlayerJump.LANDING:
                OnUpdateLanding();
                break;

            default:
                Debug.LogError($"OnStateUpdate: Invalid state {state}");
                break;
        }

    }
    #endregion

    // Méthode appelée à chaque frame dans un état
    #region On Fixed Update States
    private void OnStateFixedUpdate(PlayerJump state)
    {
        switch (state)
        {
            case PlayerJump.GROUNDED:
                OnFixedUpdateGrounded();
                break;

            case PlayerJump.JUMPING:
                OnFixedUpdateJumping();
                break;

            case PlayerJump.LANDING:
                OnFixedUpdateLanding();
                break;

            default:
                Debug.LogError($"OnStateFixedUpdate: Invalid state {state}");
                break;
        }

    }
    #endregion

    // Méthode appelée à chaque frame dans un état
    #region On Exit States
    private void OnStateExit(PlayerJump state)
    {
        switch (state)
        {
            case PlayerJump.GROUNDED:
                OnExitGrounded();
                break;

            case PlayerJump.JUMPING:
                OnExitJumping();
                break;

            case PlayerJump.LANDING:
                OnExitLanding();
                break;

            default:
                Debug.LogError($"OnStateExit: Invalid state {state}");
                break;
        }

    }
    #endregion

    // Méthode appelée pour passer d'un état à un autre
    #region On Transition to States
    private void TransitionToState(PlayerJump toState)
    {
        OnStateExit(_currentState);
        _currentState = toState;
        OnStateEnter(toState);
    }
    #endregion

    #endregion

    #region Grounded State
    private void OnEnterGrounded()
    {

    }
    private void OnUpdateGrounded()
    {
        if (_inputs.AskingJumping && _healthState == PlayerHealth.AWAKE && _playerControls.IsAttackFreezeEnded && !_playerControls.GamePause && !_pauseManager.GamePaused)
            TransitionToState(PlayerJump.JUMPING);
    }
    private void OnFixedUpdateGrounded()
    {

    }
    private void OnExitGrounded()
    {

    }
    #endregion
    #region Jumping State
    private void OnEnterJumping()
    {
        _anim.SetBool("Jump", true);
        _playerControls.StartJump();
    }
    private void OnUpdateJumping()
    {
        if (_playerControls.IsJumpEnded)
            TransitionToState(PlayerJump.LANDING);

        _playerControls.DoJump();
    }
    private void OnFixedUpdateJumping()
    {

    }
    private void OnExitJumping()
    {
        _anim.SetBool("Jump", false);
    }
    #endregion
    #region LandingState
    private void OnEnterLanding()
    {
        _anim.SetBool("Land", true);
    }
    private void OnUpdateLanding()
    {
        // If landing over
        TransitionToState(PlayerJump.GROUNDED);
    }
    private void OnFixedUpdateLanding()
    {

    }
    private void OnExitLanding()
    {
        _anim.SetBool("Land", false);
    }
    #endregion
}
