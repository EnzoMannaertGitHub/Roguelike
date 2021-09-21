using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private GameObject _gameObject = null;
    [SerializeField] private Rigidbody2D _rigidBody = null;
    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private float _minKnockbackHeight = 2f;
    [SerializeField] private float _maxKnockbackHeight = 5f;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private float _currentHealth = 0f;
    private bool _isDead = false;

    private bool _isImmune = false;
    private float _imuneTime = 0f;
    private float _flickerTime = 0f;
    private float _maxImuneTime = 2f;

    private Health _health;
    private void Start()
    {
        if (_gameObject == null)
        {
            _gameObject = gameObject;
        }

        if (_rigidBody == null)
        {
            _rigidBody = _gameObject.GetComponent<Rigidbody2D>();
        }

        _health = FindObjectOfType<Health>();
        _currentHealth = _maxHealth;
    }

    private void Update()
    {
        if (_isImmune)
        {
            _imuneTime += Time.deltaTime;
            _flickerTime += Time.deltaTime;
            if (_flickerTime > 0.1f)
            {
                _flickerTime = 0;
                _spriteRenderer.enabled = !_spriteRenderer.isVisible;
            }

            if (_imuneTime >= _maxImuneTime)
            {
                _flickerTime = 0;
                _spriteRenderer.enabled = true;

                _isImmune = false;
                _imuneTime = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 11 && other.CompareTag("Player"))
        {
            Projectile proj = other.GetComponent<Projectile>();

            if (proj != null)
            {
                Vector2 projVelocity = proj.GetVelocity();
                float knockbackHeight = Random.Range(_minKnockbackHeight, _maxKnockbackHeight);

                TakeDamage(proj.GetDamage(), new Vector2(projVelocity.x, knockbackHeight));

                proj.DestroyProjectile(true);
            }
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
        _rigidBody.velocity = new Vector2(0, 0);
        _rigidBody.AddForce(knockbackDir * 2);
    }

    public void KillEnemy()
    {
        _health.End.EnemiesKilled++;
        _isDead = true;
        _gameObject.GetComponent<Monster>().CallOnDestroy();
        Destroy(_gameObject.gameObject);
    }
}
