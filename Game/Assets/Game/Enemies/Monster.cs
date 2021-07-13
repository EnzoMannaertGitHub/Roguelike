using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private GameObject _gold;
    [SerializeField] private int _nrOfGoldDrops;

    public Breed _breed { get; private set; }
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

    public void CallOnDestroy()
    {
        for (int i = 0; i < _nrOfGoldDrops; i++)
        {
            Vector3 pos = _monsterTransform.position;
            float randX = Random.Range(pos.x - 0.15f, pos.x + 0.15f);
            Instantiate(_gold, new Vector3(randX, pos.y, 0), _monsterTransform.rotation);
        }
    }

}