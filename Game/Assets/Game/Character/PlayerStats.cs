using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Script References")]
    [SerializeField] private CharacterController2D _playerController = null;
    [SerializeField] private Health _playerHealth = null;
    [SerializeField] private PlayerAttack _playerAttack = null;

    [Header("Base Stats")]
    [SerializeField] private float _baseMaxHealth = 100f;
    [SerializeField] private float _basehealthRegen = 0f;
    [SerializeField] private float _baseAttackSpeed = 1f;
    [SerializeField] private float _baseMovementSpeed = 13f;
    [SerializeField] private float _baseJumpForce = 100f;
    [SerializeField] private float _baseDamage = 27f;
    [SerializeField] private float _baseProjectileSpeed = 5f;

    [Header("Debug")]
    [SerializeField] private bool _godMode = false;

    // Stat increases
    private float _healthIncrease = 0f;
    private float _healthPercent = 1f;
    private float _healthRegenIncrease = 0f;
    private float _healthRegenPercent = 1f;
    private float _attackSpeed = 1f;
    private float _movementSpeed = 0f;
    private float _movementPercent = 1f;
    private float _jumpForce = 0f;
    private float _jumpPercent = 1f;
    private float _damageIncrease = 0f;
    private float _damagePercent = 1f;
    private float _projectileSpeedIncrease = 0f;
    private float _projectileSpeedPercent = 1f;

    // Current stats
    private float _currentMaxHealth = 100f;
    private float _currentHealthRegen = 0f;
    private float _currentAttackSpeed = 1f;
    private float _currentMovementSpeed = 13f;
    private float _currentJumpSpeed = 100f;
    private float _currentDamage = 27f;
    private float _currentProjectileSpeed = 5f;

    private void Start()
    {
        UpdateStats();

        _playerHealth.SetHealth(_currentMaxHealth);
        _playerHealth.SetGodMode(_godMode);
    }

    private void Update()
    {
        _playerHealth.TakeDamage(-_currentHealthRegen * Time.deltaTime);
    }

    public void UpdateStats()
    {
        UpdateMaxHealth();
        UpdateHealthRegen();
        UpdateAttackSpeed();
        UpdateMovementSpeed();
        UpdateJumpForce();
        UpdateDamage();
        UpdateProjectileSpeed();
    }

    public void IncreaseHealthAmount(float amount)
    {
        _healthIncrease += amount;
        UpdateMaxHealth();
    }

    public void IncreaseHealthPercent(float percent)
    {
        _healthPercent += percent;
        UpdateMaxHealth();
    }

    public void UpdateMaxHealth()
    {
        _currentMaxHealth = (_baseMaxHealth + _healthIncrease) * _healthPercent;

        _playerHealth.SetMaxHealth(_currentMaxHealth);
    }

    public void IncreaseHealthRegenAmount(float amount)
    {
        _healthRegenIncrease += amount;
        UpdateHealthRegen();
    }

    public void IncreaseHealthRegenPercent(float percent)
    {
        _healthRegenPercent += percent;
        UpdateHealthRegen();
    }

    public void UpdateHealthRegen()
    {
        _currentHealthRegen = (_basehealthRegen + _healthRegenIncrease) * _healthRegenPercent;
    }

    public void IncreaseAttackSpeed(float percent)
    {
        _attackSpeed += percent;

        UpdateAttackSpeed();
    }

    public void UpdateAttackSpeed()
    {
        _currentAttackSpeed = _baseAttackSpeed * _attackSpeed;

        _playerAttack.SetAttackSpeed(_currentAttackSpeed);
    }

    public void IncreaseMovementSpeedAmount(float amount)
    {
        _movementSpeed += amount;

        UpdateMovementSpeed();
    }

    public void IncreaseMovementSpeedPercent(float percent)
    {
        _movementPercent += percent;

        UpdateMovementSpeed();
    }

    public void UpdateMovementSpeed()
    {
        _currentMovementSpeed = (_baseMovementSpeed + _movementSpeed) * _movementPercent;

        _playerController.SetMovementSpeed(_currentMovementSpeed);
    }

    public void IncreaseJumpForceAmount(float amount)
    {
        _jumpForce += amount;

        UpdateJumpForce();
    }

    public void IncreaseJumpForcePercent(float percent)
    {
        _jumpPercent += percent;

        UpdateJumpForce();
    }

    public void UpdateJumpForce()
    {
        _currentJumpSpeed = (_baseJumpForce + _jumpForce) * _jumpPercent;

        _playerController.SetJumpSpeed(_currentJumpSpeed);
    }

    public void IncreaseDamageAmount(float amount)
    {
        _damageIncrease += amount;

        UpdateDamage();
    }

    public void IncreaseDamagePercent(float percent)
    {
        _damagePercent += percent;

        UpdateDamage();
    }

    public void UpdateDamage()
    {
        _currentDamage = (_baseDamage + _damageIncrease) * _damagePercent;

        _playerAttack.SetDamage(_currentDamage);
    }

    public void IncreaseProjectileSpeedAmount(float amount)
    {
        _projectileSpeedIncrease += amount;

        UpdateProjectileSpeed();
    }

    public void IncreaseProjectileSpeedPercent(float percent)
    {
        _projectileSpeedPercent += percent;

        UpdateProjectileSpeed();
    }

    public void UpdateProjectileSpeed()
    {
        _currentProjectileSpeed = (_baseProjectileSpeed + _projectileSpeedIncrease) * _projectileSpeedPercent;

        _playerAttack.SetProjectileSpeed(_currentProjectileSpeed);
    }

    public float GetMaxHealth()
    {
        return _currentMaxHealth;
    }

    public float GetHealthRegen()
    {
        return _currentHealthRegen;
    }

    public float GetAttackSpeed()
    {
        return _currentAttackSpeed;
    }

    public float GetMovementSpeed()
    {
        return _currentMovementSpeed;
    }

    public float GetJumpSpeed()
    {
        return _currentJumpSpeed;
    }

    public float GetDamage()
    {
        return _currentDamage;
    }

    public float GetProjectileSpeed()
    {
        return _currentProjectileSpeed;
    }
}
