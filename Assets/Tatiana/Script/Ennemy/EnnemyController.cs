
using UnityEngine;


public class EnnemyController : MonoBehaviour
{
    #region Variables
    [SerializeField] EnnemyManager _ennemyManager;
    [SerializeField] Transform _attackPoint;

    [SerializeField] Transform _canPoint;

    PlayerControls _gamePaused;
    GameObject _player;
    Transform _transform;
    WavesController _controller;

    int _currentHealt;

    float _thinkingEndTime;
    float _attackEndTime;
    float _knockbackEndTime;
    float _delayBewteenThink;
    float _spawnRadius = 1f;



    Vector2 _finalSpawn;

    public Transform FinalSpawnPoint { get; set; }
    public bool IsSpawned { get { return Vector2.Distance(_transform.position, _finalSpawn) < 0.2f; } }
    public bool IsThinkingEnded { get { return Time.time >= _thinkingEndTime; } }
    public bool IsAttackEnded { get { return Time.time >= _attackEndTime; } }
    public bool CanThinkAgain { get { return Time.time >= _delayBewteenThink; } }
    public bool IsKnockBackEnded { get { return Time.time >= _knockbackEndTime; } }
    public bool IsTargetClose { get { return Vector2.Distance(transform.position, _player.transform.position) <= _ennemyManager.MaxDistanceDetection; } }
    public bool IsTargetRangeClose { get { return Vector2.Distance(new Vector2(_player.transform.position.x, 0) , new Vector2(_transform.position.x, 0)) <= _ennemyManager.MaxDistanceDetection; } }
    public bool IsAlignDetection { get { return Vector2.Distance(transform.position, _player.transform.position) <= _ennemyManager.MaxDistanceAlignDetection; } }
    public bool IsEnemyHit { get; set; }

    public bool IsAlive { get { return _currentHealt > 0; } }
    public int CurrentHealt { get => _currentHealt; }



    Rigidbody2D _rb;
    Rigidbody2D _canRb;

    #endregion

    // Start is called before the first frame update
    void Awake()
    {
        _transform = transform;
        _controller = GetComponentInParent<WavesController>();
        _player = FindObjectOfType<PlayerSM>().gameObject;
        _gamePaused = _player.GetComponent<PlayerControls>();
        _rb = GetComponent<Rigidbody2D>();
        _currentHealt = _ennemyManager.MaxHealth;
        StartThink();

        
    }

    private void Update()
    {
        TurnChar();

        if (!IsAlive)
            _controller.EnnemiKilled(gameObject);



    }

    private void TurnChar()
    {
        // Selon la direction du personnage on le "retourne"
        if (_rb.velocity.x < 0 && !_gamePaused.GamePause && IsKnockBackEnded)
        {
            _transform.localScale = new Vector2(-1f, 1f);
        }
        else if (_rb.velocity.x > 0 && !_gamePaused.GamePause && IsKnockBackEnded)
        {
            _transform.localScale = Vector2.one;
        }
    }

    #region Think Controller
    public void StartThink()
    {
        _thinkingEndTime = Time.time + _ennemyManager.ThinkingTime;
    }
    public void DoThink()
    {
        _rb.velocity = Vector2.zero;
    }
    #endregion

    #region Hunt Controller
    public void StartHunt()
    {
        _delayBewteenThink = Time.time + _ennemyManager.DelayThinkTime;
    }
    public void DoHunt()
    {
        _rb.velocity = (_player.transform.position - _transform.position).normalized * _ennemyManager.MoveSpeed * Time.deltaTime;
    }
    #endregion

    #region Attack Controller
    public void StartAttack()
    {
        _attackEndTime = Time.time + _ennemyManager.AttackDuration;
    }
    public void DoAttack()
    {
        // anim play attack
        Collider2D hit = Physics2D.OverlapCircle(_attackPoint.position, 0.4f, _ennemyManager.WhatIsPlayer);
        if (!hit)
            _transform.localScale = new Vector2(-_transform.localScale.x, _transform.localScale.y);

        hit = Physics2D.OverlapCircle(_attackPoint.position, 0.4f, _ennemyManager.WhatIsPlayer);
        if (hit)
            DoHit(hit);
    }
    public void DoHit(Collider2D player)
    {
        player.GetComponent<PlayerSMHealth>().HitBoxTriggered = true;
        player.GetComponent<PlayerSMHealth>().EnnemyGO = gameObject;
        // Player Hit - > Manage Health
    }
    #endregion

    #region Flee Controller
    public void StartFlee()
    {
        _delayBewteenThink = Time.time + _ennemyManager.DelayThinkTime;
    }
    public void DoFlees()
    {
        _rb.velocity = (_transform.position - _player.transform.position).normalized * _ennemyManager.MoveSpeed * Time.deltaTime;
    }
    #endregion

    #region Hurt Controller
    public void StartHurt()
    {
        IsEnemyHit = false;
        _knockbackEndTime = Time.time + _ennemyManager.KnockbackDuration;
        _currentHealt--;
    }
    public void DoHurt()
    {
        Vector2 veloc = Vector2.left * -_player.transform.localScale * _ennemyManager.KnocbackForce;

        float accel = _ennemyManager.KnockbackEasing.Evaluate(GetKnockBackprogress());

        _rb.velocity = veloc * accel;
    }
    private float GetKnockBackprogress()
    {
        // Get knockback actual time
        float KnockTime = Time.time - (_knockbackEndTime - _ennemyManager.KnockbackDuration);
        // Get the progress between 0 to 1
        float KnockProgress = KnockTime / _ennemyManager.KnockbackDuration;

        return KnockProgress;
    }
    public void StartDeath()
    {
        DoDrop();
    }
    public void DoDeath()
    {
        _rb.velocity = Vector2.zero;
    }
    #endregion

    #region Spawn Controller
    public void StartSpawn()
    {
        _finalSpawn = (Vector2)FinalSpawnPoint.position + Random.insideUnitCircle * _spawnRadius;
    }
    public void DoSpawn()
    {
        _rb.velocity = (_finalSpawn - (Vector2)_transform.position).normalized * _ennemyManager.MoveSpeed * Time.deltaTime;
    }
    #endregion

    #region Drop Controller
    public void DoDrop()
    {
        int quantityDropped = Random.Range(0, _ennemyManager.LootQuantity);
        for (int i = 0; i < quantityDropped; i++)
        {
            Rigidbody2D drop = Instantiate(_ennemyManager.Loots[Random.Range(0, _ennemyManager.Loots.Length)], _transform.position, Quaternion.identity);
            GiveLootForce(drop);
        }
    }
    public void GiveLootForce(Rigidbody2D drop)
    {
        drop.AddForce(Vector2.one * Random.Range(-1f, 1f) * _ennemyManager.LootDropForce);
    }
    #endregion

    #region Align Controller

    public void StartAlign()
    {
        _delayBewteenThink = Time.time + _ennemyManager.DelayThinkTime;
    }

    public void DoAlign()
    {
        if (IsAlignDetection)
        {
            _rb.velocity = (new Vector2(0f, _player.transform.position.y) - new Vector2(0f, _transform.position.y)).normalized * _ennemyManager.MoveSpeed * Time.deltaTime;
        }
        else
        {
            _rb.velocity = (_player.transform.position - _transform.position).normalized * _ennemyManager.MoveSpeed * Time.deltaTime;
        }

    }

    #endregion

    #region Throw Controller

    public void StartThrow()
    {
        _delayBewteenThink = Time.time + _ennemyManager.DelayThinkTime;

        _canRb = Instantiate(_ennemyManager.CanBluePrefab, _canPoint.transform.position, Quaternion.identity);

        _canRb.velocity = (new Vector2( _player.transform.position.x,0) - new Vector2( _transform.position.x,0)).normalized * _ennemyManager.Speed * Time.deltaTime;
    }

    public void DoThrow()
    {


        // _canRb.AddForce(transform.forward * _ennemyManager.Speed, ForceMode2D.Impulse);

    }

    #endregion
}
