using UnityEngine;
public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject _fireSocket;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private Animator _animator;
    [SerializeField] private Animator _armAnimator;
    [SerializeField] private Transform _armPivot;
    [SerializeField] private ParticleSystem _shootParticles;
    [SerializeField] private CharacterController2D _controller;


    private bool _CanShoot = true;
    public bool CanShoot { get { return _CanShoot; } }
    private bool _shoot = false;
    private float _damage = 1f;
    private float _projectileSpeed = 5f;
    private int _projectiles = 1;
    private Coroutine _chargeDelay = null;
    private Vector3 aimDirection;
    private Camera _mainCam;
    private float _arrowSpaceBetween = 0.06f;

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
        _animator.SetFloat("AttackSpeed", newSpeed);
        _armAnimator.SetFloat("AttackSpeed", newSpeed);
    }

    public void SetProjectileAmount(int amount)
    {
        _projectiles = amount;
    }

    private void HandleShooting()
    {
        if (!_CanShoot)
            return;

        Ray ray = _mainCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitinfo = new RaycastHit();
        Physics.Raycast(ray, out hitinfo, 1000, LayerMask.GetMask("Shoot"));

        Vector3 point = hitinfo.point;

        aimDirection = (point - transform.position).normalized;
        if (aimDirection.x < 0)
        {
            Vector3 theScale = transform.localScale;
            theScale.x = -1;
            transform.localScale = theScale;
            _controller.FacingRight = false;
        }
        else
        {
            Vector3 theScale = transform.localScale;
            theScale.x = 1;
            transform.localScale = theScale;
            _controller.FacingRight = true;
        }
        _CanShoot = false;
        _animator.SetTrigger("Attack");
        _armAnimator.SetTrigger("Attack");

        if (_playerMovement.GetGroundState())
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

        if (_controller.FacingRight)
            _armPivot.eulerAngles = new Vector3(0, 0, angle);
        else
            _armPivot.eulerAngles = new Vector3(0, 180, -angle);
    }

    public void ShootArrow()
    {
        if (_CanShoot)
            return;

        _CanShoot = true;

        _shootParticles.Emit(10);

        float heightOffset = ((_projectiles % 2) == 1) ? (-(_projectiles - 1) / 2 * _arrowSpaceBetween) : (-_projectiles / 2 * _arrowSpaceBetween);
        for (int i = 0; i < _projectiles; i++)
        {
            var fireSocketTransform = _fireSocket.transform;
            Vector3 position = fireSocketTransform.position + fireSocketTransform.up * heightOffset;
            GameObject newArrow = Instantiate(_bulletPrefab, position, Quaternion.identity);

            Projectile projectileScript = newArrow.GetComponent<Projectile>();
            if (projectileScript != null)
            {
                projectileScript.InitProjectile(aimDirection, _damage, _projectileSpeed);
                projectileScript.SetDamage(_damage);
            }

            heightOffset += _arrowSpaceBetween;
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
