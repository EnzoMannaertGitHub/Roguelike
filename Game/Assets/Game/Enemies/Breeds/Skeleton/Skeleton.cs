using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Skeleton : Breed
{
    [SerializeField] private float _baseSpeed = 3f;
    [SerializeField] private float _chargeMultiplier = 2f;
    [SerializeField] protected Animator _animator = null;

    public float _chargeSpeedMultiplier { get; private set; } = 2f;
    private float _currentSpeed = 3f;
    public bool _hitPlayer { get; private set; } = false;
    private bool _isCharging = false;

    public bool _playerSeen = false;
    private bool _attacking = false;

    private void Start()
    {
        _currentSpeed = _baseSpeed;
        _chargeSpeedMultiplier = _chargeMultiplier;
    }

    public Skeleton(float damage) : base(damage)
    { }

    public override void UpdateBehavior()
    {
        switch (_movementState)
        {
            case States.patrol:
                Move();
                break;
            case States.charging:
                ChargeAttack();
                break;
            case States.attack:
                break;
        }

        if (_rigidbody == null)
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        _animator.SetFloat("SpeedX", _rigidbody.velocity.x);
        _animator.SetFloat("SpeedMultiplier", _isCharging ? _chargeMultiplier : 1f);
    }

    public void StartCharge()
    {
        if (_movementState == States.patrol)
        {
            _movementState = States.charging;
            _currentSpeed = _baseSpeed * _chargeSpeedMultiplier;
            _isCharging = true;
        }
    }

    public void StopCharge()
    {
        if (_movementState == States.charging)
        {
            _movementState = States.patrol;
            _currentSpeed = _baseSpeed;
            _isCharging = false;
        }
    }

    public void TriggerAttack()
    {
        if (!_attacking)
        {
            Attack();
        }
    }

    private void ChargeAttack()
    {
        if (_playerTransform == null)
        {
            _movementState = States.patrol;
            return;
        }

        Vector3 direction = _playerTransform.position - transform.position;

        if (direction.x > 0f)
        {
            _rigidbody.velocity = new Vector2(1f, 0f) * _currentSpeed;
        }
        else
        {
            _rigidbody.velocity = new Vector2(-1f, 0f) * _currentSpeed;
        }
    }

    protected override void Attack()
    {
        _rigidbody.velocity = Vector2.zero;
        _movementState = States.attack;
        _currentSpeed = _baseSpeed;
        _isCharging = false;
        _animator.SetTrigger("Attack");
        _attacking = true;
    }

    public void StopAttack()
    {
        _movementState = States.patrol;
        _hitPlayer = false;
        _attacking = false;
    }

    protected override void Move()
    {
        //needs to be implemented
    }

    public float GetDamage()
    {
        return _damage;
    }

    public Transform GetPlayerTransform()
    {
        return _playerTransform;
    }

    public override void OnPlayerHit(GameObject g)
    {
    }
}
