using UnityEngine;
public class ChampBat : Bat
{
    [SerializeField] private GameObject _projectile;
    protected override void CustomAttack()
    {
        Vector2 direction = (_targetTransform.position - _monsterTransform.position).normalized;
        float speed = 1.25f;

        if (_hasAttacked)
        {
                _rigidbody.velocity = direction * speed / 4;

                _elapsedHitTime += Time.deltaTime;
                if (_elapsedHitTime >= _recoverTime)
                {
                    _elapsedHitTime = 0f;

                    _hitSomething = false;
                    _hasCharged = false;
                    _hasAttacked = false;

                    _movementState = States.patrol;
                    _rigidbody.velocity = new Vector2(0, 0);
                }
            return;
        }
        GameObject proj = Instantiate(_projectile, _monsterTransform.position, _monsterTransform.rotation);
        proj.GetComponent<BatProjectile>().shoot(direction);
        _hasAttacked = true;
    }
}