using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private PlayerMovement _movement;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            Projectile proj = other.GetComponent<Projectile>();

            if (proj != null)
            {
                Vector2 projVelocity = proj.GetVelocity();
                float knockbackHeight = Random.Range(_minKnockbackHeight, _maxKnockbackHeight);

                GetHit(proj.GetDamage(), new Vector2(projVelocity.x, knockbackHeight));
            }
        }
    }

    public void GetHit(float damage, Vector2 knockbackDir)
    {
        if (!_isImmune)
        {
            TakeDamage(damage);
            _movement.HandleKnockBack(knockbackDir);
            _isImmune = true;
        }
    }

    public void TakeDamage(float damage)
    {
        _currentHealth = Mathf.Clamp(_currentHealth - damage, 0f, _maxHealth);

        if (_currentHealth == 0 && !_isDead)
        {
            Die();
        }
    }

    private void Die()
    {
        _isDead = true;
    }
}
