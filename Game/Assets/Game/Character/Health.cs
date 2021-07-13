using UnityEngine;

public class Health : MonoBehaviour
{
    private float _maxHealth = 100f;
    private float _health;
    
    [SerializeField] private PlayerMovement _movement;
    [SerializeField] private SpriteRenderer _playerSprite;
    private bool _isImmune = false;
    private float _imuneTime = 0f;
    private float _maxImuneTime = 2f;
    private float _drawTime = 0f;
    private bool _isDead = false;
    private bool _godMode = false;

    public void SetMaxHealth(float newHealth)
    {
        _maxHealth = newHealth;
    }

    public void SetHealth(float newHealth)
    {
        _health = newHealth;
    }

    public void SetGodMode(bool state)
    {
        _godMode = state;
    }

    private void Update()
    {
        if(_isImmune)
        {
            _imuneTime += Time.deltaTime;
            _drawTime += Time.deltaTime;
            if (_drawTime > 0.1f)
            {
                _drawTime = 0;
                _playerSprite.enabled = !_playerSprite.isVisible;
            }

            if(_imuneTime >= _maxImuneTime)
            {
                _isImmune = false;
                _playerSprite.enabled = true;
                _drawTime = 0;
                _imuneTime = 0;
            }
        }
    }
    public void GetHit(float damage, Vector2 knockbackDir)
    {
        if (_isDead || _godMode)
            return;

        if(!_isImmune)
        {
            TakeDamage(damage);
            _movement.HandleKnockBack(knockbackDir);
            _isImmune = true;
        }
    }

    public void TakeDamage(float amount)
    {
        if (_isDead || _godMode)
            return;

        _health = Mathf.Clamp(_health - amount, 0f, _maxHealth);

        if (_health == 0f)
        {
            KillPlayer();
        }
    }

    public void KillPlayer()
    {
        _isDead = true;
        Destroy(gameObject);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 11 && other.CompareTag("Enemy"))
        {
            Projectile proj = other.GetComponent<Projectile>();

            if (proj != null)
            {
                Vector2 projVelocity = proj.GetVelocity();
                float knockbackHeight = 1f;

                GetHit(proj.GetDamage(), new Vector2(projVelocity.x, knockbackHeight));

                proj.DestroyProjectile();
            }
        }
    }
}
