using UnityEngine;
public class portal : MonoBehaviour
{
    private GameObject _player;
    private Vector3 _spawn;
    [SerializeField] private bool _returnPortal = false;
    [SerializeField] private EnemySpawner _enemySpawner;
    private void Start()
    {
        _player = FindObjectOfType<PlayerMovement>().gameObject;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player") || collision.gameObject.layer != 0)
            return;

        _spawn = new Vector3(14, 9, 0);

        if (_returnPortal)
        {
            _spawn = new Vector3(3, 2, 0);
            _enemySpawner.ClearEnemies();
            _spawn = new Vector3(3, 1, 0);
            LevelManager.Instance.LoadNewLevel();
        }
        _player.transform.position = _spawn;
    }
}