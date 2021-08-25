using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] protected float _damage;
    [SerializeField] protected GameObject _gold;
    [SerializeField] protected int _nrOfGoldDrops;

    [SerializeField] private Breed _breed;
    [SerializeField] private bool _debugMode = false;
    protected Transform _monsterTransform;
    protected bool _initialised = false;

    [SerializeField] protected Transform _playerTransform;

    public void InitMonster(Transform playerTransform)
    {
        _playerTransform = playerTransform;

        _breed.PlayerTransform = _playerTransform;
        _monsterTransform = gameObject.transform;
        _breed.MonsterTransform = _monsterTransform.transform;
        _breed.Rigidbody = GetComponent<Rigidbody2D>();
        _breed.Damage = _damage;

        _initialised = true;
    }

    protected virtual void Update()
    {
        if (!_debugMode)
        {
            if (!_initialised)
            {
                Debug.LogError(gameObject.name + " not initialised.");
                _initialised = true;
                return;
            }
        }

        _breed.UpdateBehavior();
    }

    protected void OnTriggerStay2D(Collider2D collision)
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
