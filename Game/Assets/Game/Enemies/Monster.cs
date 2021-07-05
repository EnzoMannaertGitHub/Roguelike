using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] private float _damage;
    private Breed _breed;
    private Transform _monsterTransform;
    private bool _initialised = false;

    private Transform _playerTransform;

    public void InitMonster(Breed breed, Transform playerTransform)
    {
        _breed = breed;
        _playerTransform = playerTransform;

        _breed.PlayerTransform = _playerTransform;
        _monsterTransform = gameObject.transform;
        _breed.MonsterTransform = _monsterTransform.transform;
        _breed.Rigidbody = GetComponent<Rigidbody2D>();
        _breed.Damage = _damage;

        _initialised = true;
    }

    private void Update()
    {
        if (!_initialised)
        {
            Debug.LogError("Monster not initialised");
            return;
        }

        _breed.UpdateBehavior();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
       _breed.OnPlayerHit(collision.gameObject);
    }
}
