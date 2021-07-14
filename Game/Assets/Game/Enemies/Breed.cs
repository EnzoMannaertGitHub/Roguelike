using UnityEngine;
abstract public class Breed : MonoBehaviour
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
    public States _movementState { get; protected set; } = States.patrol;

    protected bool _istargetSet = false;

    protected float _damage;
    public float Damage { set { _damage = value; } }

    protected Transform _playerTransform;
    public Transform PlayerTransform { set { _playerTransform = value; } }

    protected Transform _monsterTransform;
    public Transform MonsterTransform { set { _monsterTransform = value; } }

    protected Rigidbody2D _rigidbody;
    public Rigidbody2D Rigidbody { get { return _rigidbody; } set { _rigidbody = value; } }

    #endregion

    abstract public void UpdateBehavior();

    abstract protected void Attack();

    abstract protected void Move();

    abstract public void OnPlayerHit(GameObject g);
}