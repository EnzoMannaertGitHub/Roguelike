using System.Collections;
using UnityEngine;
public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject _fireSocket;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private Animator _animator;
    
    private bool _CanShoot = true;
    private bool _shoot = false;
    private float _damage = 1f;
    private float _projectileSpeed = 5f;
    private float _chargeBaseDuration = 0.9f;
    private float _chargeDuration = 0.9f;
    private Coroutine _chargeDelay = null;

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

    public void SetDamage(float newDamage)
    {
        _damage = newDamage;
    }

    public void SetProjectileSpeed(float newSpeed)
    {
        _projectileSpeed = newSpeed;
    }

    public void SetAttackSpeed(float newSpeed)
    {
        _chargeDuration = _chargeBaseDuration / newSpeed;

        _animator.SetFloat("AttackSpeed", newSpeed);
    }    

    private void HandleShooting()
    {
        if (!_CanShoot)
            return;

        _CanShoot = false;
        _animator.SetTrigger("Attack");
        _playerMovement.SetCanMove(false);
    }

    public void ShootArrow()
    {
        if (_CanShoot)
            return;

        _CanShoot = true;

        var fireSocketTransform = _fireSocket.transform;
        GameObject newArrow = Instantiate(_bulletPrefab, fireSocketTransform.position, Quaternion.identity);

        Projectile projectileScript = newArrow.GetComponent<Projectile>();
        if (projectileScript != null)
        {
            Vector2 direction = gameObject.GetComponent<PlayerMovement>().IsLeft ? new Vector2(-1, 0) : new Vector2(1, 0);
            projectileScript.InitProjectile(direction, _damage, _projectileSpeed);
            projectileScript.SetDamage(_damage);
        }

        _playerMovement.SetCanMove(true);
    }

    private void OnDestroy()
    {
        if (_chargeDelay != null)
        {
            StopCoroutine(_chargeDelay);
            _chargeDelay = null;
        }
    }
}