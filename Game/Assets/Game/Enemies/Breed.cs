using UnityEngine;
public class Breed : MonoBehaviour
{
    public enum States
    {
        patrol,
        attack,
        charging
    }
    #region constructors
    public Breed()
    {
        _damage = 0;
    }
   public Breed(float damage)
    {
        _damage = damage;
    }
    #endregion

    #region variables
    protected States _movementState = States.patrol;

    protected bool _istargetSet = false;

    protected float _damage;
    public float Damage { set { _damage = value; } }

    protected Transform _playerTransform;
    public Transform PlayerTransform { set { _playerTransform = value; } }

    protected Transform _monsterTransform;
    public Transform MonsterTransform { set { _monsterTransform = value; } }

    protected Rigidbody2D _rigidbody;
    public Rigidbody2D Rigidbody { set { _rigidbody = value; } }

    #endregion

    virtual public void UpdateBehavior()
    {
        //needs to be implemented
    }
    virtual protected void Attack()
    {
        //needs to be implemented
    }

    virtual protected void Move()
    {
        //needs to be implemented
    }

    virtual public void OnPlayerHit(GameObject g)
    {
        //needs to be implemented
    }
}