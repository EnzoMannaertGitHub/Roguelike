using UnityEngine;

public class Bat : Breed
{
    protected Transform _targetTransform;

    [SerializeField] protected float _focusTime = 1f;
    [SerializeField] protected float _recoverTime = 1f;
    protected bool _hasCharged = false;
    protected bool _hasAttacked = false;
    protected bool _hitSomething = false;
    protected float _elapsedFocusTime = 0f;
    protected float _elapsedHitTime = 0f;

    private bool _elevating = false;
    private float _waitSec = 0f;
    protected float _elapsedElevationTime = 0f;

    public override void UpdateBehavior()
    {
        HandleStates();
        Move();
    }

    protected override void Move()
    {
        if (_elevating)
            return;

        switch (_movementState)
        {
            case States.patrol:
                _istargetSet = false;
                _monsterTransform.Translate(new Vector3((Mathf.Sin(Time.realtimeSinceStartup) * 0.6f) * Time.deltaTime,
                                                       0,
                                                       0));
                break;
            case States.attack:
                if (!_istargetSet)
                {
                    _targetTransform = _playerTransform;
                    _istargetSet = true;
                }
                Attack();
                break;
            case States.charging:
                Charge();
                break;
        }
    }

    protected override void Attack()
    {
        CustomAttack();
    }

    public override void OnPlayerHit(GameObject g)
    {
        if (_elevating)
            return;

        if (g.CompareTag("Player"))
        {         
            Vector2 dir = new Vector2();
            if (_monsterTransform.position.x <= _playerTransform.position.x)
                dir.x = 1;
            else
                dir.x = -1;

            if (_monsterTransform.position.y <= _playerTransform.position.y)
                dir.y = 1;
            else
                dir.y = -1;

            g.GetComponent<Health>().GetHit(_damage, dir);
        }

        if (!g.CompareTag("Enemy"))
        {
            _rigidbody.velocity = new Vector2(0, 0);
            _hitSomething = true;
        }
    }
    private void HandleStates()
    {
        if (_elevating)
        {
            HandleElevation();
            return;
        }

        if (_playerTransform.position.y < _monsterTransform.position.y &&
            Vector2.Distance(_playerTransform.position, _monsterTransform.position) < 2)
        {
            if (_hasCharged)
            {
                _movementState = States.attack;
            }
            else
                _movementState = States.charging;
        }
        else if(_movementState != States.charging)
        {
            _rigidbody.velocity = new Vector2(0, 0);
            _movementState = States.patrol;
        }
    }
    private void Charge()
    {
        _elapsedFocusTime += Time.deltaTime;
        if (_elapsedFocusTime >= _focusTime)
        {
            _elapsedFocusTime = 0f;
            _hasCharged = true;
            _movementState = States.attack;
        }
    }
    virtual protected void CustomAttack()
    {
        if (_elevating)
        {
            HandleElevation();
            return;
        }

        if (_hasAttacked)
        {
            if (_hitSomething)
            {
                _rigidbody.velocity = new Vector2(0, 0.4f);
                _elevating = true;
                _hitSomething = false;
                _hasAttacked = false;
            }
            return;
        }

        Vector2 direction = (_targetTransform.position - _monsterTransform.position).normalized;
        float speed = 1.25f;
        _rigidbody.velocity = direction * speed;

        _hasAttacked = true;
    }
    private void HandleElevation()
    {
        _waitSec += Time.deltaTime;
         if (_waitSec >= 1f)
        {
            if (_hitSomething || ElevationDone())
            {
                _waitSec = 0;
                _elapsedElevationTime = 0;

                _movementState = States.patrol;

                _hasAttacked = false;
                _elevating = false;
            }
        }
    }
    private bool ElevationDone()
    {
        _elapsedElevationTime += Time.deltaTime;
        if (_elapsedElevationTime > 0.5f)
        {
            return true;
        }
        return false;
    }
}