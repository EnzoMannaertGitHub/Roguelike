using UnityEngine;

public class Bat : Breed
{
    private Transform _targetTransform;
    private bool _istargetSet = false;
    private bool _hasCharged = false;
    public Bat(float damage) : base(damage)
    {}

    public override void UpdateBehavior()
    {
        Move();
        HandleStates();
    }

    protected override void Move()
    {
        switch (MovementState)
        {
            case States.patrol:
                _istargetSet = false;

                MonsterTransform.Translate(new Vector3((Mathf.Sin(Time.realtimeSinceStartup) * 0.6f) * Time.deltaTime,
                                                       0, 
                                                       0));
                break;
            case States.attack:
                if(!_istargetSet)
                {
                    _targetTransform = PlayerTransform;
                    _istargetSet = true;
                }
                Attack();
                break;
        }
    }

    protected override void Attack()
    {
        Vector2 direction = (_targetTransform.position - MonsterTransform.position).normalized;
        MonsterTransform.Translate(new Vector3(direction.x * Time.deltaTime,
                                               direction.y * Time.deltaTime, 
                                               0));
    }

    public override void OnPlayerHit(GameObject g)
    {
        Debug.Log("Bat hit player");
        Vector2 dir = new Vector2();
        if (MonsterTransform.position.x <= PlayerTransform.position.x)
            dir.x = 1;
        else
            dir.x = -1;

        if (MonsterTransform.position.y <= PlayerTransform.position.y)
            dir.y = 1;
        else
            dir.y = -1;

        g.GetComponent<Health>().GetHit(Damage, dir);
    }

    private void HandleStates()
    {
        if (Vector2.Distance(PlayerTransform.position, MonsterTransform.position) < 2)
            MovementState = States.attack;
        else
            MovementState = States.patrol;
    }
}