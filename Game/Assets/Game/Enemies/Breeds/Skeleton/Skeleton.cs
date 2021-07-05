using UnityEngine;

public class Skeleton : Breed
{
    private Transform _targetTransform;
    public Skeleton(float damage) : base(damage)
    { }

    public override void UpdateBehavior()
    {
        //needs to be implemented
    }

    protected override void Attack()
    {
        //needs to be implemented
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