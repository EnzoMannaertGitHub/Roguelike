using UnityEngine;

public class Skeleton : Breed
{
    private Transform _targetTransform;
    
    [SerializeField] private Animator _animator;
    private bool _attacking = false;

    public Skeleton(float damage) : base(damage)
    { }

    public override void UpdateBehavior()
    {
        //needs to be implemented
    }

    protected override void Attack()
    {
        _animator.SetTrigger("Attack");
        _attacking = true;
    }

    public void StopAttack()
    {
        if (_attacking)
        {
            _attacking = false;
        }
    }

    protected override void Move()
    {
        //needs to be implemented
    }

    public override void OnPlayerHit(GameObject g)
    {
        //needs to be implemented
    }
}