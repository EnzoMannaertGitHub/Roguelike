using UnityEngine;

public class Skeleton : Breed
{
    [SerializeField] private float _horizontalAggroRange = 4f;
    [SerializeField] private float _verticalAggroRange = 2f;
    [SerializeField] private float _attackRange = 2f;
    [SerializeField] private float _baseSpeed = 3f;
    [SerializeField] private float _chargeMultiplier = 2f;
    [SerializeField] protected Animator _animator = null;

    public float _chargeSpeedMultiplier { get; private set; } = 2f;
    private float _currentSpeed = 3f;
    public bool _hitPlayer { get; private set; } = false;
    private bool _isCharging = false;

    private void Start()
    {
        _currentSpeed = _baseSpeed;
        _chargeSpeedMultiplier = _chargeMultiplier;
    }

    public Skeleton(float damage) : base(damage)
    { }

    public override void UpdateBehavior()
    {
        if (_movementState == States.patrol)
        {
            if (Mathf.Abs(_playerTransform.position.x - transform.position.x) <= _horizontalAggroRange && Mathf.Abs(_playerTransform.position.y - transform.position.y) <= _verticalAggroRange)
            {
                _movementState = States.charging;
                _currentSpeed = _baseSpeed * _chargeSpeedMultiplier;
            }
            else
            {
                Move();
            }
        }

        if (_movementState == States.charging)
        {
            ChargeAttack();
        }

        _animator.SetFloat("SpeedX", _rigidbody.velocity.x);
        _animator.SetFloat("SpeedMultiplier", _isCharging ? _chargeMultiplier : 1f);
    }

    private void ChargeAttack()
    {
        if (_playerTransform == null)
        {
            _movementState = States.patrol;
            return;
        }

        Vector3 direction = _playerTransform.position - transform.position;
        direction.z = 0f;

        if (direction.magnitude <= _attackRange)
        {
            Attack();
            return;
        }

        _isCharging = true;

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
    }

    public void StopAttack()
    {
        _movementState = States.patrol;
        _hitPlayer = false;
    }

    protected override void Move()
    {
        //needs to be implemented
    }

    public override void OnPlayerHit(GameObject g)
    {
        if (_playerTransform == null)
            return;

        if (g.CompareTag("Player") && _movementState == States.attack)
        {
            Vector2 dir = new Vector2();
            if (transform.position.x <= _playerTransform.position.x)
                dir.x = 1;
            else
                dir.x = -1;

            if (transform.position.y <= _playerTransform.position.y)
                dir.y = 1;
            else
                dir.y = -1;

            g.GetComponent<Health>().GetHit(_damage, dir);
        }

        if (!g.CompareTag("Enemy"))
        {
            _rigidbody.velocity = new Vector2(0, 0);
        }
    }
}