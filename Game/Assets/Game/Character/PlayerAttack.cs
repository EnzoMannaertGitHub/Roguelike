using UnityEngine;
public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject _fireSocket;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private Animator _animator;
    [SerializeField] private Animator _armAnimator;
    [SerializeField] private Transform _armPivot;
    [SerializeField] private CharacterController2D _controller;

    private bool _CanShoot = true;
    private bool _shoot = false;
    private float _damage = 1f;
    private float _projectileSpeed = 5f;
    private float _chargeBaseDuration = 0.9f;
    private float _chargeDuration = 0.9f;
    private Coroutine _chargeDelay = null;

    private Camera _mainCam;
    private void Awake()
    {
        _mainCam = Camera.main;

        if (_mainCam == null) Debug.LogError("BulletBehavior.cs: mainCam is null!");
    }

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
        _armAnimator.SetTrigger("Attack");
        _playerMovement.SetCanMove(false);

        HandleArmRotation();
    }

    private void HandleArmRotation()
    {
        Ray ray = _mainCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitinfo = new RaycastHit();
        Physics.Raycast(ray, out hitinfo, 1000, LayerMask.GetMask("Shoot"));

        Vector3 point = hitinfo.point;

        Vector3 aimDirection = (point - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        if (angle > 90)
        {
            if (_controller.FacingRight)
                _armPivot.eulerAngles = new Vector3(0, 180, 0);
            else
                _armPivot.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            if (_controller.FacingRight)
                _armPivot.eulerAngles = new Vector3(0, 0, 0);
            else
                _armPivot.eulerAngles = new Vector3(0, 180, 0);
        }

        Debug.Log(angle);
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