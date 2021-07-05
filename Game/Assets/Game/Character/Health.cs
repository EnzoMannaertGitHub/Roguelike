using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 100f;
    private float _health;
    [SerializeField] private PlayerMovement _movement;
    [SerializeField] private SpriteRenderer _playerSprite;
    private bool _isImmune = false;
    private float _imuneTime = 0f;
    private float _maxImuneTime = 2f;
    private float _drawTime = 0f;
    private bool _isDead = false;

    private void Start()
    {
        _health = _maxHealth;
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
        if (_isDead)
            return;

        if(!_isImmune)
        {
            TakeDamage(damage);
            _movement.HandleKnockBack(knockbackDir);
            _isImmune = true;
        }
    }

    private void TakeDamage(float amount)
    {
        if (_isDead)
            return;

        _health = Mathf.Clamp(_health - amount, 0f, _maxHealth);

        if (_health == 0)
        {
            KillPlayer();
        }
    }

    public void KillPlayer()
    {
        _isDead = true;
        Destroy(gameObject);
    }
}
