using UnityEngine;

public class DoppleGangersSM : MonoBehaviour
{
    private DoppleGangersState _currentState;
    private EnnemyController _ennemyController;
    private Animator _animator;

    private void Awake()
    {
        _ennemyController = GetComponent<EnnemyController>();
        _animator = GetComponentInChildren<Animator>();
    }
    private void Start()
    {
        OnEnterSpawn();
    }
    private void Update()
    {
        OnStateUpdate(_currentState);
        Debug.Log(_currentState);
    }
    private void FixedUpdate()
    {
        OnStateFixedUpdate(_currentState);
    }

    //----------------------------------------------------Main State pattern---------------------------------------------
    #region Main State pattern

    #region On Enter States
    private void OnStateEnter(DoppleGangersState state)
    {
        switch (state)
        {
            case DoppleGangersState.SPAWN:
                OnEnterSpawn();
                break;
            case DoppleGangersState.THINK:
                OnEnterThink();
                break;
            case DoppleGangersState.ALIGN:
                OnEnterAlign();
                break;
            case DoppleGangersState.THROW:
                OnEnterThrow();
                break;
            case DoppleGangersState.FLEES:
                OnEnterFlees();
                break;
            case DoppleGangersState.HURT:
                OnEnterFlees();
                break;
            case DoppleGangersState.DEAD:
                OnEnterDead();
                break;
                ;
            default:
                Debug.LogError("OnStateEnter: Invalid state " + state);
                break;
        }
    }
    #endregion

    #region On Update States
    private void OnStateUpdate(DoppleGangersState state)
    {
        switch (state)
        {
            case DoppleGangersState.SPAWN:
                OnUpdateSpawn();
                break;
            case DoppleGangersState.THINK:
                OnUpdateThink();
                break;
            case DoppleGangersState.ALIGN:
                OnUpdateAlign();
                break;
            case DoppleGangersState.THROW:
                OnUpdateThrow();
                break;
            case DoppleGangersState.FLEES:
                OnUpdateFlees();
                break;
            case DoppleGangersState.HURT:
                OnUpdateHurt();
                break;
            case DoppleGangersState.DEAD:
                OnUpdateDead();
                break;

            default:
                Debug.LogError("OnStateEnter: Invalid state " + state.ToString());
                break;
        }
    }


    #endregion

    #region On Fixed Update States
    private void OnStateFixedUpdate(DoppleGangersState state)
    {
        switch (state)
        {
            case DoppleGangersState.SPAWN:
                OnFixedUpdateSpawn();
                break;
            case DoppleGangersState.THINK:
                OnFixedUpdateThink();
                break;
            case DoppleGangersState.ALIGN:
                OnFixedUpdateAlign();
                break;
            case DoppleGangersState.THROW:
                OnFixedUpdateThrow();
                break;
            case DoppleGangersState.FLEES:
                OnFixedUpdateFlees();
                break;
            case DoppleGangersState.HURT:
                OnFixedUpdateHurt();
                break;
            case DoppleGangersState.DEAD:
                OnFixedUpdateDead();
                break;

            default:
                Debug.LogError("OnStateFixedUpdate: Invalid state " + state.ToString());
                break;
        }
    }

    #endregion

    #region On Exit States
    private void OnStateExit(DoppleGangersState state)
    {
        switch (state)
        {
            case DoppleGangersState.SPAWN:
                OnExitSpawn();
                break;
            case DoppleGangersState.THINK:
                OnExitThink();
                break;
            case DoppleGangersState.ALIGN:
                OnExitAlign();
                break;
            case DoppleGangersState.THROW:
                OnExitThrow();
                break;
            case DoppleGangersState.FLEES:
                OnExitFlees();
                break;
            case DoppleGangersState.HURT:
                OnExitHurt();
                break;
            case DoppleGangersState.DEAD:
                OnExitDead();
                break;
            default:
                Debug.LogError("OnStateExit: Invalid state " + state.ToString());
                break;
        }
    }
    #endregion

    #region Transition To States
    private void TransitionToState(DoppleGangersState toState)
    {
        OnStateExit(_currentState);
        _currentState = toState;
        OnStateEnter(toState);
    }
    #endregion

    #endregion
    //----------------------------------------------------Spawn---------------------------------------------
    #region Spawn State
    private void OnEnterSpawn()
    {
        _animator.SetBool("Hunt", true);
        _ennemyController.StartSpawn();
    }
    private void OnUpdateSpawn()
    {
        if (_ennemyController.IsSpawned)
            TransitionToState(DoppleGangersState.THINK);
    }
    private void OnFixedUpdateSpawn()
    {
        _ennemyController.DoSpawn();
    }
    private void OnExitSpawn()
    {
        _animator.SetBool("Hunt", false);
    }
    #endregion
    //----------------------------------------------------Think---------------------------------------------
    #region Think State
    private void OnEnterThink()
    {
        _ennemyController.StartThink();

        _animator.SetBool("Think", true);
    }
    private void OnUpdateThink()
    {
        if (_ennemyController.IsEnemyHit)
            TransitionToState(DoppleGangersState.HURT);
        else if (_ennemyController.IsThinkingEnded)
        {
            if (_ennemyController.IsTargetRangeClose)
            {
                switch (GetRandomAction())
                {
                    case <= 0.15f:
                        TransitionToState(DoppleGangersState.THINK);
                        break;
                    case <= 0.3f:
                        TransitionToState(DoppleGangersState.FLEES);
                        break;
                    default:
                        TransitionToState(DoppleGangersState.THROW);
                        break;
                }
            }
            else
            {
                switch (GetRandomAction())
                {
                    case <= 0.15f:
                        TransitionToState(DoppleGangersState.THINK);
                        break;
                    case <= 0.3f:
                        TransitionToState(DoppleGangersState.FLEES);
                        break;
                    default:
                        TransitionToState(DoppleGangersState.ALIGN);
                        break;
                }
            }
        }

        _ennemyController.DoThink();
    }
    private void OnFixedUpdateThink()
    {

    }
    private void OnExitThink()
    {
        _animator.SetBool("Think", false);
    }

    private float GetRandomAction()
    {
        float tempFloat = Random.Range(0f, 1f);

        return tempFloat;
    }
    #endregion
    //----------------------------------------------------Align---------------------------------------------
    #region Align

    private void OnEnterAlign()
    {
        _animator.SetBool("Hunt", true);
        _ennemyController.StartAlign();


    }
    private void OnUpdateAlign()
    {
        if (_ennemyController.IsEnemyHit)
            TransitionToState(DoppleGangersState.HURT);
        else if (_ennemyController.IsTargetClose || _ennemyController.CanThinkAgain)
            TransitionToState(DoppleGangersState.THINK);

    }
    private void OnFixedUpdateAlign()
    {
        _ennemyController.DoAlign();
    }
    private void OnExitAlign()
    {
        _animator.SetBool("Hunt", false);
    }

    #endregion
    //---------------------------------------------------- Flees---------------------------------------------
    #region flees
    private void OnEnterFlees()
    {
        _ennemyController.StartFlee();
        _animator.SetBool("Flees", true);

    }
    private void OnUpdateFlees()
    {
        if (_ennemyController.IsEnemyHit)
            TransitionToState(DoppleGangersState.HURT);
        else if (_ennemyController.CanThinkAgain)
            TransitionToState(DoppleGangersState.THINK);

    }
    private void OnFixedUpdateFlees()
    {
        _ennemyController.DoFlees();
    }
    private void OnExitFlees()
    {
        _animator.SetBool("Flees", false);
    }

    #endregion
    //---------------------------------------------------- THROW---------------------------------------------
    #region Throw

    private void OnEnterThrow()
    {
        _ennemyController.StartThrow();

    }
    private void OnUpdateThrow()
    {
        if(_ennemyController.CanThinkAgain)
        {
            TransitionToState(DoppleGangersState.THINK);
        }
    }
    private void OnFixedUpdateThrow()
    {
        _ennemyController.DoThrow();


    }
    private void OnExitThrow()
    {

    }

    #endregion
    //---------------------------------------------------- Hurt---------------------------------------------
    #region Hurt

    private void OnEnterHurt()
    {
        _animator.SetTrigger("Hurt");
        _ennemyController.StartHurt();
    }
    private void OnUpdateHurt()
    {

        if (_ennemyController.IsKnockBackEnded)
        {
            if (_ennemyController.IsAlive)
                TransitionToState(DoppleGangersState.THINK);
            else
                TransitionToState(DoppleGangersState.DEAD);
        }
    }
    private void OnFixedUpdateHurt()
    {

        _ennemyController.DoHurt();
    }
    private void OnExitHurt()
    {

    }

    #endregion
    //---------------------------------------------------- Dead---------------------------------------------
    #region Dead
    private void OnEnterDead()
    {
        _animator.SetTrigger("Death");
    }
    private void OnUpdateDead()
    {
        _ennemyController.DoDeath();
    }
    private void OnFixedUpdateDead()
    {

    }

    private void OnExitDead()
    {

    }
    #endregion





}


