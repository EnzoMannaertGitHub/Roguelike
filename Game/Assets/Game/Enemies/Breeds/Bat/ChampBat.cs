using UnityEngine;
public class ChampBat : Bat
{
    [SerializeField] private GameObject _projectile;
    protected override void CustomAttack()
    {
        Vector2 direction = (_targetTransform.position - _monsterTransform.position).normalized;

        float speed = 1.25f;
        _rigidbody.velocity = direction * speed;

    }
}