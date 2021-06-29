using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float _damage;

    private Transform _monsterTransform;
    public void Awake()
    {
        _breed.PlayerTransform = _playerTransform;
        _monsterTransform = gameObject.transform;
        _breed.MonsterTransform = _monsterTransform.transform;
        _breed.Rigidbody = GetComponent<Rigidbody2D>();
        _breed.Damage = _damage;
    }

    [SerializeField] private Breed _breed;

    private void Update()
    {
        //_breed.UpdateBehavior();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            _breed.OnPlayerHit(collision.gameObject);
    }
}
