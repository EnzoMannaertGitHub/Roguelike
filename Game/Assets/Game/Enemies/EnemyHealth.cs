using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 100f;
    private PlayerMovement _playerMovement;
    [SerializeField] private float _minKnockbackHeight = 2f;
    [SerializeField] private float _maxKnockbackHeight = 5f;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Rigidbody2D _rigidbody;

    
    private float _currentHealth = 0f;
    private bool _isDead = false;

    private bool _isImmune = false;
    private float _imuneTime = 0f;
    private float _flickerTime = 0f;
    private float _maxImuneTime = 2f;

    public void SetPlayerMovement(PlayerMovement movement)
    {
        _playerMovement = movement;
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

        _rigidbody.AddForce(knockbackDir * 40);
    }

    public void KillEnemy()
    {
        _isDead = true;
        Destroy(gameObject);
    }
}
