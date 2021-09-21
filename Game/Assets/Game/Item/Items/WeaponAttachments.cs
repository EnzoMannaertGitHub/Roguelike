using UnityEngine;
[CreateAssetMenu(fileName = "New Attachment", menuName = "Inventory/Attachments")]
public class WeaponAttachments : Item
{
    [Header("Stats")]
    [SerializeField] private float _health = 0f;
    [SerializeField] private float _healthPercent = 0f;
    [SerializeField] private float _healthRegen = 0f;
    [SerializeField] private float _healthRegenPercent = 0f;
    [SerializeField] private float _attackSpeed = 0f;
    [SerializeField] private float _movementSpeed = 0f;
    [SerializeField] private float _movementPercent = 0f;
    [SerializeField] private float _jumpForce = 0f;
    [SerializeField] private float _jumpPercent = 0f;
    [SerializeField] private float _damageIncrease = 0f;
    [SerializeField] private float _damagePercent = 0f;
    [SerializeField] private float _projectileSpeedIncrease = 0f;
    [SerializeField] private float _projectileSpeedPercent = 0f;
    [SerializeField] private int _extraProjectiles = 0;

    public override void Use()
    {
        PlayerStats playerStats = Inventory.Instance._playerStats;
        if (_health != 0f)
        {
            playerStats.IncreaseHealthAmount(_health);
        }
        if (_healthPercent != 0f)
        {
            playerStats.IncreaseHealthPercent(_healthPercent);
        }
        if (_healthRegen != 0f)
        {
            playerStats.IncreaseHealthRegenAmount(_healthRegen);
        }
        if (_healthRegenPercent != 0f)
        {
            playerStats.IncreaseHealthRegenPercent(_healthRegenPercent);
        }
        if (_attackSpeed != 0f)
        {
            playerStats.IncreaseAttackSpeed(_attackSpeed);
        }
        if (_movementSpeed != 0f)
        {
            playerStats.IncreaseMovementSpeedAmount(_movementSpeed);
        }
        if (_movementPercent != 0f)
        {
            playerStats.IncreaseMovementSpeedPercent(_movementPercent);
        }
        if (_jumpForce != 0f)
        {
            playerStats.IncreaseJumpForceAmount(_jumpForce);
        }
        if (_jumpPercent != 0f)
        {
            playerStats.IncreaseJumpForcePercent(_jumpPercent);
        }
        if (_damageIncrease != 0f)
        {
            playerStats.IncreaseDamageAmount(_damageIncrease);
        }
        if (_damagePercent != 0f)
        {
            playerStats.IncreaseDamagePercent(_damagePercent);
        }
        if (_projectileSpeedIncrease != 0f)
        {
            playerStats.IncreaseProjectileSpeedAmount(_projectileSpeedIncrease);
        }
        if (_projectileSpeedPercent != 0f)
        {
            playerStats.IncreaseProjectileSpeedPercent(_projectileSpeedPercent);
        }
        if (_extraProjectiles != 0)
        {
            playerStats.IncreaseProjectilesAmount(_extraProjectiles);
        }

        base.Use();
    }

    public override void OnClear()
    {
        PlayerStats playerStats = Inventory.Instance._playerStats;
        if (_health != 0f)
        {
            playerStats.IncreaseHealthAmount(-_health);
        }
        if (_healthPercent != 0f)
        {
            playerStats.IncreaseHealthPercent(-_healthPercent);
        }
        if (_healthRegen != 0f)
        {
            playerStats.IncreaseHealthRegenAmount(-_healthRegen);
        }
        if (_healthRegenPercent != 0f)
        {
            playerStats.IncreaseHealthRegenPercent(-_healthRegenPercent);
        }
        if (_attackSpeed != 0f)
        {
            playerStats.IncreaseAttackSpeed(-_attackSpeed);
        }
        if (_movementSpeed != 0f)
        {
            playerStats.IncreaseMovementSpeedAmount(-_movementSpeed);
        }
        if (_movementPercent != 0f)
        {
            playerStats.IncreaseMovementSpeedPercent(-_movementPercent);
        }
        if (_jumpForce != 0f)
        {
            playerStats.IncreaseJumpForceAmount(-_jumpForce);
        }
        if (_jumpPercent != 0f)
        {
            playerStats.IncreaseJumpForcePercent(-_jumpPercent);
        }
        if (_damageIncrease != 0f)
        {
            playerStats.IncreaseDamageAmount(-_damageIncrease);
        }
        if (_damagePercent != 0f)
        {
            playerStats.IncreaseDamagePercent(-_damagePercent);
        }
        if (_projectileSpeedIncrease != 0f)
        {
            playerStats.IncreaseProjectileSpeedAmount(-_projectileSpeedIncrease);
        }
        if (_projectileSpeedPercent != 0f)
        {
            playerStats.IncreaseProjectileSpeedPercent(-_projectileSpeedPercent);
        }
        if (_extraProjectiles != 0)
        {
            playerStats.IncreaseProjectilesAmount(-_extraProjectiles);
        }

        base.OnClear();
    }
}
