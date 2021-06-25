using UnityEngine;
public class PrimaryWeapon : MonoBehaviour
{
    [SerializeField] private GameObject _fireSocket;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _fireRate = 0.5f;

    private bool _CanShoot = true;
    private float _elapsedShootSec = 0f;

    private bool _shoot = false;

    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            _shoot = true;
        }
        if(Input.GetButtonUp("Fire1"))
        {
            _shoot = false;
        }
    }
    private void FixedUpdate()
    {
        if(_shoot)
        {
            Shoot();
        }
        if(!_CanShoot)
            HandleFireRate();
    }
    void Shoot()
    {
        if(_CanShoot)
        {
            var fireSocketTransform = _fireSocket.transform;
            GameObject newArrow = Instantiate(_bulletPrefab, fireSocketTransform.position, Quaternion.identity);

            Projectile projectileScript = newArrow.GetComponent<Projectile>();
            if (projectileScript != null)
            {
                Vector2 direction = gameObject.GetComponent<PlayerMovement>().IsLeft ? new Vector2(-1, 0) : new Vector2(1, 0);
                projectileScript.InitProjectile(direction, !gameObject.GetComponent<PlayerMovement>().IsMoving);
            }

            _CanShoot = false;
        }
    }
    void HandleFireRate()
    {
        _elapsedShootSec += Time.deltaTime;
        if(_elapsedShootSec > _fireRate)
        {
            _elapsedShootSec = 0;
            _CanShoot = true;
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