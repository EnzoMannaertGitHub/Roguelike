using UnityEngine;
public class PrimaryWeapon : MonoBehaviour
{
    [SerializeField] private GameObject _fireSocket;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _fireRate = 0.5f;


    private bool _CanShoot = true;
    private float _elapsedShootSec = 0f;
    private float _range = 1.5f;
    private float _speed = 2f;
    public float _damage = 1f;

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
            _bulletPrefab.GetComponent<RegularBullet>().SetRange(_range);
            _bulletPrefab.GetComponent<RegularBullet>().SetSpeed(_speed);
            GameObject newBullet = Instantiate(_bulletPrefab, fireSocketTransform.position, Quaternion.identity);

            Vector2 direction;
            if (gameObject.GetComponent<PlayerMovement>().IsLeft)
                direction = new Vector2(-1, 0);
            else
                direction = new Vector2(1, 0);

            newBullet.GetComponent<RegularBullet>().SetDirection(direction, !gameObject.GetComponent<PlayerMovement>().IsMoving);


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

    public void AdjustDamage(float damage)
    {
        _damage += damage;
    }
    public void AdjustFirerate(float fireRate)
    {
        _fireRate -= fireRate;
    }
    public void AdjustRange(float range)
    {
        _range += range;
    }
    public void AdjustSpeed(float speed)
    {
        _speed += speed;
    }
}