using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //-------------------
    // TO BE REPLACED:
    [SerializeField] private List<Vector3> _spawnPositions = new List<Vector3>();
    private void Start()
    {
        if (!_playerFound)
        {
            FindPlayer();
        }

        for (int index = 0; index < _spawnPrefabs.Count; index++)
        {
            if (_spawnPositions.Count > index)
            {
                SpawnEnemy(index, _spawnPositions[index]);
            }
            else
            {
                SpawnEnemy(index, Vector3.zero);
            }
        }
    }
    //-------------------

    [SerializeField] private Transform _enemyTransform = null;
    [SerializeField] private List<GameObject> _spawnPrefabs = new List<GameObject>();   // Prefabs of all enemies
    [SerializeField] private List<GameObject> _breedObjects = new List<GameObject>();   // Breed GameObjects (MUST BE SAME ORDER)

    private Transform _playerTransform = null;
    private PlayerMovement _playerMovement = null;
    private bool _playerFound = false;

    private void Awake()
    {
        FindPlayer();
    }

    private void FindPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            _playerTransform = player.transform;
            _playerMovement = player.GetComponent<PlayerMovement>();

            if (_playerTransform != null && _playerMovement != null)
                _playerFound = true;
        }
    }

    private void Update()
    {
        if (!_playerFound)
        {
            FindPlayer();
        }
    }

    private void SpawnEnemy(int id, Vector3 location)
    {
        if (!_playerFound)
            return;

        GameObject enemy = Instantiate(_spawnPrefabs[id], location, Quaternion.identity);

        if (_enemyTransform != null)
        {
            enemy.transform.SetParent(_enemyTransform);
        }

        Monster monsterScript = enemy.GetComponent<Monster>();
        if (monsterScript != null)
        {
            monsterScript.InitMonster(_breedObjects[id].GetComponent<Breed>(), _playerTransform);
        }

        EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.SetPlayerMovement(_playerMovement);
        }
    }
}