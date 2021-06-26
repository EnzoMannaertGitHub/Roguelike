using System.Collections;
using UnityEngine;
public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject _fireSocket;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _fireRateDelay = 0.5f;
    [SerializeField] private float _chargeTime = 0.9f;

    private bool _CanShoot = true;
    private bool _shoot = false;
    private Coroutine _chargeDelay = null;
    private Coroutine _firerateDelay = null;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            _shoot = true;
        }
        if (Input.GetButtonUp("Fire1"))
        {
            _shoot = false;
        }
    }

    private void FixedUpdate()
    {
        if (_shoot)
        {
            HandleShooting();
        }
    }

    private void HandleShooting()
    {
        if (!_CanShoot)
            return;

        _CanShoot = false;
        _animator.SetTrigger("Attack");
        _playerMovement.SetCanMove(false);

        if (_chargeDelay != null)
        {
            StopCoroutine(_chargeDelay);
            _chargeDelay = null;
        }
        _chargeDelay = StartCoroutine("HandleChargeDelay");
    }

    private IEnumerator HandleChargeDelay()
    {
        yield return new WaitForSeconds(_chargeTime);

        ShootArrow();
    }

    private void ShootArrow()
    {
        var fireSocketTransform = _fireSocket.transform;
        GameObject newArrow = Instantiate(_bulletPrefab, fireSocketTransform.position, Quaternion.identity);

        Projectile projectileScript = newArrow.GetComponent<Projectile>();
        if (projectileScript != null)
        {
            Vector2 direction = gameObject.GetComponent<PlayerMovement>().IsLeft ? new Vector2(-1, 0) : new Vector2(1, 0);
            projectileScript.InitProjectile(direction);
        }

        _playerMovement.SetCanMove(true);

        if (_firerateDelay != null)
        {
            StopCoroutine(_firerateDelay);
            _firerateDelay = null;
        }
        _firerateDelay = StartCoroutine("HandleFireRate");
    }

    private IEnumerator HandleFireRate()
    {
        yield return new WaitForSeconds(_fireRateDelay);

        _CanShoot = true;
    }

    private void OnDestroy()
    {
        if (_firerateDelay != null)
        {
            StopCoroutine(_firerateDelay);
            _firerateDelay = null;
        }
    }

    //public void ChangeDamage(float damage)
    //{
    //    _damage += damage;
    //}

    //public void ChangeDamage(float fireRate)
    //{
    //    _fireRate += fireRate;
    //}

    //public void ChangeDamage(float range)
    //{
    //    _range += range;
    //}
    //public void ChangeDamage(float speed)
    //{
    //    _speed += speed;
    //}
}