using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private float _minKnockbackHeight = 2f;
    [SerializeField] private float _maxKnockbackHeight = 5f;

    private float _currentHealth = 0f;
    private bool _isDead = false;

    private bool _isImmune = false;
    private float _imuneTime = 0f;
    private float _maxImuneTime = 2f;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    private void Update()
    {
        if (_isImmune)
        {
            _imuneTime += Time.deltaTime;

            if (_imuneTime >= _maxImuneTime)
            {
                _isImmune = false;
                _imuneTime = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("arrow hit");
        if (other.CompareTag("Projectile"))
        {
            Projectile proj = other.GetComponent<Projectile>();

            if (proj != null)
            {
                Vector2 projVelocity = proj.GetVelocity();
                float knockbackHeight = Random.Range(_minKnockbackHeight, _maxKnockbackHeight);

                TakeDamage(proj.GetDamage(), new Vector2(projVelocity.x, knockbackHeight));
            }

            proj.DestroyProjectile();
        }
    }

    public void GetHit(float damage, Vector2 knockbackDir)
    {
        if (!_isImmune)
        {
            TakeDamage(damage, knockbackDir);
        }
    }

    public void TakeDamage(float damage, Vector2 knockbackDir)
    {
        _currentHealth = Mathf.Clamp(_currentHealth - damage, 0f, _maxHealth);

        if (damage > 0)
        {
            _isImmune = true;
        }

        if (_currentHealth == 0 && !_isDead)
        {
            KillEnemy();
        }
    }

    public void KillEnemy()
    {
        _isDead = true;
        Destroy(gameObject);
    }
}
