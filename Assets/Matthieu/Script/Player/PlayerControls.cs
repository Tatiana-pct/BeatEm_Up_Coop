using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    #region Variables
    [Header("SPEED CONTROLER")]
    [Space(10)]
    [SerializeField] float _walkSpeed = 5f;
    [SerializeField] float _runSpeed = 8f;
    [Space(10)]
    [Header("JUMP CONTROLER")]
    [Space(10)]
    [SerializeField] float _jumpForce = 5f;
    [SerializeField] float _jumpDuration = .5f;
    [SerializeField] AnimationCurve _JumpEasing;
    [Space(10)]
    [SerializeField] PlayerManager _playerManager;
    [SerializeField] float _idleAnimTime;

    [SerializeField] GameObject _vfxDust;

    Rigidbody2D _rb;
    PlayerInputs _inputs;
    Transform _transform;
    Transform _childTransform;
    Animator _anim;

    float _jumpEndTime;
    float _invicibleEndTime;
    float _knockbackEndTime;
    float _attackEndTime;
    float _respawnEndTime;
    float _deadEndTime;
    float _idleTimer;

    bool _gameOver;
    bool _gamePause;

    [SerializeField] Transform _levelSpawnPoint;
    [SerializeField] Transform _attackPoint;
    [SerializeField] LayerMask _whatIsHitable;

    [SerializeField] LevelsManager _levelManager;
    [SerializeField] PauseManager _pauseManager;

    public bool IsJumpEnded { get { return Time.time >= _jumpEndTime; } }
    public bool IsInvicibleEnded { get { return Time.time >= _invicibleEndTime; } }
    public bool IsKnockBackEnded { get { return Time.time >= _knockbackEndTime; } }
    public bool IsAttackFreezeEnded { get { return Time.time >= _attackEndTime; } }
    public bool IsDeadOver { get { return Time.time >= _deadEndTime; } }
    public bool IsRespawnEnded { get { return Time.time >= _respawnEndTime; } }
    public bool GamePause { get => _gamePause; }
    #endregion

    private void Update()
    {
        if (!_gamePause && _gameOver)
        {
            _deadEndTime = Time.time + 1f;
            _anim.SetTrigger("Death");
            _gamePause = true;
        }

        if (IsDeadOver && _gameOver)
            _levelManager.Death();

    }

    private void Awake()
    {
        _transform = transform;
        _anim = GetComponentInChildren<Animator>();
        _childTransform = _transform.GetChild(0);
        _inputs = GetComponent<PlayerInputs>();
        _rb = GetComponent<Rigidbody2D>();
        _idleTimer = Time.time + _idleAnimTime;
    }

    #region Movement Controls
    public void DoWalk()
    {
        Vector2 veloc = _inputs.ClampedMovement * _walkSpeed;

        Applymovement(veloc);
    }

    public void DoSprint()
    {
        Vector2 veloc = _inputs.NormalizedMovement * _runSpeed;

        Applymovement(veloc);
    }

    #region Idle Controls
    public void StartIdle()
    {
        _idleTimer = Time.time + _idleAnimTime;
    }
    public void DoIdle()
    {
        // Idle -> No velocity
        Vector2 veloc = Vector2.zero;

        Applymovement(veloc);

        if (Time.time > _idleTimer)
            DoIdleAnim();
    } 
    public void DoIdleAnim()
    {
        StartIdle();
        _anim.SetFloat("RandomIdle", Random.Range(0f, 1f));
        _anim.SetTrigger("IdleAnim");
    }
    #endregion

    private void Applymovement(Vector2 velocity)
    {
        // On applique la velocity
        _rb.velocity = velocity;

        TurnCharacter();
    }

    private void TurnCharacter()
    {
        if (!_pauseManager.GamePaused)
        {
            // Selon la direction du personnage on le "retourne"
            if (_inputs.Movement.x < 0 && !GamePause)
            {
                _transform.localScale = new Vector2(-1f, 1f);
            }
            else if (_inputs.Movement.x > 0 && !GamePause)
            {
                _transform.localScale = Vector2.one;
            } 
        }
    }
    #endregion

    #region Jump Controls
    public void StartJump()
    {
        // Initalize Jump
        _jumpEndTime = Time.time + _jumpDuration;
        GameObject vfx = Instantiate(_vfxDust, _transform.position + Vector3.up*.5f, Quaternion.identity);
        Destroy(vfx, 0.5f);
    }

    public void DoJump()
    {
        // Move the Body along his local Y axis to simulate Jump
        _childTransform.localPosition = new Vector2(_childTransform.localPosition.x, _JumpEasing.Evaluate(GetJumpProgress()) * _jumpForce);
        _anim.SetFloat("JumpProgress", GetJumpProgress());
    }

    private float GetJumpProgress()
    {
        // Get Jump actual time
        float JumpTime = Time.time - (_jumpEndTime - _jumpDuration);
        // Get the progress between 0 to 1
        float JumpProgress = JumpTime / _jumpDuration;

        return JumpProgress;
    }
    #endregion

    #region Hurt Controls
    public void StartHurt()
    {
        _invicibleEndTime = Time.time + _playerManager.InvicibleDuration;
        _knockbackEndTime = Time.time + _playerManager.KnockbackDuration;
        _playerManager.CurrentHealth--;
    }
    public void DoHurt(GameObject ennemy)
    {
        Vector2 veloc = Vector2.left * -ennemy.transform.localScale * _playerManager.KnockbackForce;

        float accel = _playerManager.KnockbackEasing.Evaluate(GetKnockBackprogress());

        Applymovement(veloc * accel);

    }

    private float GetKnockBackprogress()
    {
        // Get knockback actual time
        float KnockTime = Time.time - (_knockbackEndTime - _playerManager.KnockbackDuration);
        // Get the progress between 0 to 1
        float KnockProgress = KnockTime / _playerManager.KnockbackDuration;

        return KnockProgress;
    }
    #endregion

    #region Loot Controls
    public void DoLoot(CollectiblesManager collectible)
    {
        _playerManager.Score += collectible.ScoreValue;
        _playerManager.CurrentHealth = Mathf.Clamp(_playerManager.CurrentHealth + collectible.HealthValue, 0, _playerManager.MaxHealth);
    }
    #endregion

    #region Health Controls
    public void StartResurrect()
    {
        _respawnEndTime = Time.time + _playerManager.RespawnTime;
    }
    public void DoResurect()
    {
        if (Time.time > _respawnEndTime)
        {
            _transform.position = _levelSpawnPoint.position;
            _playerManager.CurrentHealth = _playerManager.MaxHealth;
            _playerManager.CurrentLifeCount--;
        }
    }
    public void DoDead()
    {
        // Load Game Over Scene
        _gameOver = true;
        _playerManager.CurrentLifeCount--;
    }
    #endregion

    #region Attack Controls
    public void StartAttack()
    {
        _attackEndTime = Time.time + _playerManager.AttackTime;
    }
    public void DoAttack()
    {
        // anim play attack
        Collider2D hit = Physics2D.OverlapCircle(_attackPoint.position, 0.4f, _whatIsHitable);
        if (hit)
            DoHit(hit);
    } 
    public void DoHit(Collider2D hit)
    {
        if (hit.gameObject.CompareTag("Ennemy"))
            hit.GetComponent<EnnemyController>().IsEnemyHit = true;
        // Ennemie Hit - > Manage Health
        else if (hit.gameObject.CompareTag("Destructible"))
            hit.GetComponent<DestructibleObjects>().Hit();
    }
    #endregion
}
