using UnityEngine;
public class portal : MonoBehaviour
{
    private GameObject _player;
    [SerializeField] private Transform _spawn;
    [SerializeField] private bool _returnPortal = false;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private Shop _shop;
    private void Start()
    {
        _player = FindObjectOfType<PlayerMovement>().gameObject;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player") || collision.gameObject.layer != 0)
            return;

        if (_returnPortal)
        {
            _enemySpawner.ClearEnemies();
            LevelManager.Instance.LoadNewLevel();
            _shop.IsInShop = false;
            return;
        }
        _player.transform.position = _spawn.position;
        _shop.IsInShop = true;
    }
}