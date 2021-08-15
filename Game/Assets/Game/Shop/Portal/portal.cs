using UnityEngine;
public class portal : MonoBehaviour
{
    private GameObject _player;
    private Vector3 _spawn;
    [SerializeField] bool _returnPortal = false;
    private void Start()
    {
        _player = FindObjectOfType<PlayerMovement>().gameObject;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player") || collision.gameObject.layer != 0)
            return;

        _spawn = new Vector3(14, 8, 0);

        if (_returnPortal)
        {
            _spawn = new Vector3(3, 1, 0);
            //FindObjectOfType<EnemySpawner>().ClearEnemies();
            LevelManager.Instance.LoadNewLevel();
        }
        _player.transform.position = _spawn;
    }
}