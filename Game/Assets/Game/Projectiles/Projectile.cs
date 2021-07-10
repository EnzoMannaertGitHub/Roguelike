using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Projectile : MonoBehaviour
{
    [SerializeField] protected float _damage = 1f;
    [SerializeField] protected float _speed = 10f;
    [SerializeField] protected float _range = -1f;
    [SerializeField] protected float _timeToLive = 10f;

    [SerializeReference] protected Rigidbody2D _rigidBody;
    protected Camera _mainCam;

    protected Vector2 _direction;
    protected Vector2 _startPos;
    protected float _lifeSpan = 0f;
    protected Coroutine _destroyCoroutine = null;

    protected bool _rangeCheck = false;
    protected bool _initialised = false;

    protected void Update()
    {
        if (!_initialised)
        {
            Debug.LogError("projectile not initialised");
            _initialised = true; // set to true to avoid spam
        }

        HandleRange();
    }

    protected abstract void Shoot();

    protected void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.layer == 3)
        {
            Destroy(gameObject);
        }
    }

    protected void HandleRange()
    {
        if (!_rangeCheck)
            return;

        if (Vector3.Distance(transform.position, _startPos) >= _range)
        {
            DestroyProjectile();
        }
    }

    public Vector2 GetVelocity()
    {
        return _rigidBody.velocity;
    }

    public void InitProjectile(Vector2 direction, float damage = 0f, float speed = 0f, float range = 0f, bool isMousePointer = false, float timeToLive = 0f)
    {
        SetDirection(direction, isMousePointer);

        if (damage != 0f)
        {
            SetDamage(damage);
        }
        else 
        {
            SetDamage(_damage);
        }

        if (speed != 0f)
        {
            SetSpeed(speed);
        }
        else 
        {
            SetSpeed(_speed);
        }

        if (range != 0f)
        {
            SetRange(range);
        }
        else 
        {
            SetRange(_range);
        }
        
        if (timeToLive != 0f)
        {
            SetTimeToLive(timeToLive);
        }
        else 
        {
            SetTimeToLive(_timeToLive);
        }

        Shoot();
        _initialised = true;
    }

    public void SetDamage(float damage)
    {
        _damage = damage;
    }

    public float GetDamage()
    {
        return _damage;
    }

    public void SetSpeed(float speed)
    {
        _speed = speed;
    }

    public void SetRange(float range)
    {
        _rangeCheck = (range > 0f);
        _range = range;
    }

    public void SetTimeToLive(float time)
    {
        _lifeSpan = time;

        if (_destroyCoroutine == null)
        {
            _destroyCoroutine = StartCoroutine("DestroyDelayed");
        }
        else
        {
            StopCoroutine(_destroyCoroutine);
            _destroyCoroutine = null;

            _destroyCoroutine = StartCoroutine("DestroyDelayed");
        }
    }

    private IEnumerator DestroyDelayed()
    {
        yield return new WaitForSeconds(_lifeSpan);

        DestroyProjectile();
        _destroyCoroutine = null;
    }

    public void DestroyProjectile()
    {
        if (_destroyCoroutine != null)
        {
            StopCoroutine(_destroyCoroutine);
            _destroyCoroutine = null;
        }

        Destroy(gameObject);
    }

    public void SetDirection(Vector2 direction, bool isMousePointer)
    {
        _mainCam = Camera.main;

        if (_mainCam == null) Debug.LogError("Bullet.cs: mainCam is null!");

        if (isMousePointer)
        {
            var mousePos = _mainCam.ScreenToWorldPoint(Input.mousePosition);
            Vector2 point = new Vector2(mousePos.x, mousePos.y);
            _startPos = new Vector2(transform.position.x, transform.position.y);
            _direction = (point - _startPos).normalized;
        }
        else
        {
            _startPos = new Vector2(transform.position.x, transform.position.y);
            _direction = direction;
        }

        float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
        transform.Rotate(0, 0, angle);
    }
}