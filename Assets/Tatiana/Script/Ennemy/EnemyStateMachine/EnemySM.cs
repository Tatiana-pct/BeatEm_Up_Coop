using UnityEngine;

public class EnemySM : MonoBehaviour
{
    private EnnemyState _currentState;
    private EnnemyController _ennemyController;
    private Animator _animator;
    [SerializeField] PauseManager _pauseManager;

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
    }
    private void FixedUpdate()
    {
        OnStateFixedUpdate(_currentState);
    }

    //----------------------------------------------------Main State pattern---------------------------------------------
    #region Main State pattern

    #region On Enter States
    private void OnStateEnter(EnnemyState state)
    {
        switch (state)
        {
            case EnnemyState.SPAWN:
                OnEnterSpawn();
                break;
            case EnnemyState.THINK:
                OnEnterThink();
                break;
            case EnnemyState.HUNT:
                OnEnterHunt();
                break;
            case EnnemyState.DEAD:
                OnEnterDead();
                break;
            case EnnemyState.ATTACK:
                OnEnterAttack();
                break;
            case EnnemyState.HURT:
                OnEnterHurt();
                break;
            case EnnemyState.FLEES:
                OnEnterFlees();
                break;
            default:
                Debug.LogError("OnStateEnter: Invalid state " + state);
                break;
        }
    }
    #endregion

    #region On Update States
    private void OnStateUpdate(EnnemyState state)
    {
        switch (state)
        {
            case EnnemyState.SPAWN:
                OnUpdateSpawn();
                break;
            case EnnemyState.HUNT:
                OnUpdateHunt();
                break;
            case EnnemyState.DEAD:
                OnUpdateDead();
                break;
            case EnnemyState.ATTACK:
                OnUpdateAttack();
                break;
            case EnnemyState.HURT:
                OnUpdateHurt();
                break;
            case EnnemyState.THINK:
                OnUpdateThink();
                break;
            case EnnemyState.FLEES:
                OnUpdateFlees();
                break;
            default:
                Debug.LogError("OnStateEnter: Invalid state " + state.ToString());
                break;
        }
    }


    #endregion

    #region On Fixed Update States
    private void OnStateFixedUpdate(EnnemyState state)
    {
        switch (state)
        {
            case EnnemyState.SPAWN:
                OnFixedUpdateSpawn();
                break;
            case EnnemyState.HUNT:
                OnFixedUpdateHunt();
                break;
            case EnnemyState.DEAD:
                OnFixedUpdateDead();
                break;
            case EnnemyState.ATTACK:
                OnFixedUpdateAttack();
                break;
            case EnnemyState.HURT:
                OnFixedUpdateHurt();
                break;
            case EnnemyState.THINK:
                OnFixedUpdateThink();
                break;
            case EnnemyState.FLEES:
                OnFixedUpdateFlees();
                break;
            default:
                Debug.LogError("OnStateFixedUpdate: Invalid state " + state.ToString());
                break;
        }
    }

    #endregion

    #region On Exit States
    private void OnStateExit(EnnemyState state)
    {
        switch (state)
        {
            case EnnemyState.SPAWN:
                OnExitSpawn();
                break;
            case EnnemyState.HUNT:
                OnExitHunt();
                break;
            case EnnemyState.DEAD:
                OnExitDead();
                break;
            case EnnemyState.ATTACK:
                OnExitAttack();
                break;
            case EnnemyState.HURT:
                OnExitHurt();
                break;
            case EnnemyState.THINK:
                OnExitThink();
                break;
            case EnnemyState.FLEES:
                OnExitFlees();
                break;
            default:
                Debug.LogError("OnStateExit: Invalid state " + state.ToString());
                break;
        }
    }
    #endregion

    #region Transition To States
    private void TransitionToState(EnnemyState toState)
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
            TransitionToState(EnnemyState.THINK);
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
        if (!_pauseManager.GamePaused)
        {
            if (_ennemyController.IsEnemyHit)
                TransitionToState(EnnemyState.HURT);
            else if (_ennemyController.IsThinkingEnded)
            {
                if (_ennemyController.IsTargetClose)
                {
                    switch (GetRandomAction())
                    {
                        case <= 0.15f:
                            TransitionToState(EnnemyState.THINK);
                            break;
                        case <= 0.3f:
                            TransitionToState(EnnemyState.FLEES);
                            break;
                        default:
                            TransitionToState(EnnemyState.ATTACK);
                            break;
                    }
                }
                else
                {
                    switch (GetRandomAction())
                    {
                        case <= 0.15f:
                            TransitionToState(EnnemyState.THINK);
                            break;
                        case <= 0.3f:
                            TransitionToState(EnnemyState.FLEES);
                            break;
                        default:
                            TransitionToState(EnnemyState.HUNT);
                            break;
                    }
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
    //----------------------------------------------------Attack---------------------------------------------
    #region Attack State
    private void OnEnterAttack()
    {
        _ennemyController.StartAttack();
        _ennemyController.DoAttack();
        _animator.SetTrigger("Attack");
        _animator.SetFloat("RandomAttack", Random.Range(0f, 1f));
    }
    private void OnUpdateAttack()
    {
        if (_ennemyController.IsAttackEnded || _pauseManager.GamePaused)
            TransitionToState(EnnemyState.THINK);
    }
    private void OnFixedUpdateAttack()
    {

    }

    private void OnExitAttack()
    {

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
            TransitionToState(EnnemyState.HURT);
        else if (_ennemyController.CanThinkAgain || _pauseManager.GamePaused)
            TransitionToState(EnnemyState.THINK);

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
    //---------------------------------------------------- Hunt---------------------------------------------
    #region hunt

    private void OnEnterHunt()
    {
        _animator.SetBool("Hunt", true);
        _ennemyController.StartHunt();
    }
    private void OnUpdateHunt()
    {
        if (_ennemyController.IsEnemyHit)
            TransitionToState(EnnemyState.HURT);
        else if (_ennemyController.IsTargetClose || _ennemyController.CanThinkAgain || _pauseManager.GamePaused)
            TransitionToState(EnnemyState.THINK);
    }
    private void OnFixedUpdateHunt()
    {
        _ennemyController.DoHunt();

    }
    private void OnExitHunt()
    {
        _animator.SetBool("Hunt", false);
    }

    #endregion
    //---------------------------------------------------- Hurt---------------------------------------------
    #region hurt

    private void OnEnterHurt()
    {
        _animator.SetTrigger("Hurt");
        _ennemyController.StartHurt();
    }
    private void OnUpdateHurt()
    {

        if (_ennemyController.IsKnockBackEnded)
        {
            if (_ennemyController.IsAlive || _pauseManager.GamePaused)
                TransitionToState(EnnemyState.THINK);
            else
                TransitionToState(EnnemyState.DEAD);
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
        _ennemyController.StartDeath();
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


