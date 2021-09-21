using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _spawnPositions;
    public List<Transform> SpawnPositions { get { return _spawnPositions; } set { _spawnPositions = value; } }

    List<GameObject> _enemies = new List<GameObject>();
    [SerializeField] private Transform _enemyTransform = null;

    [SerializeField] private List<GameObject> _spawnPrefabs = new List<GameObject>();          // Prefabs of all non-champ enemies
    [SerializeField] private List<GameObject> _spawnPrefabsChamps = new List<GameObject>();   // Prefabs of all champ enemies

    private Transform _playerTransform = null;
    private PlayerMovement _playerMovement = null;
    private bool _playerFound = false;
    private int _numberOfEnemiesPerSpawn = 1;

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

    private void SpawnEnemy(int id, Vector3 location, bool isChamp)
    {
        if (!_playerFound)
            return;

        GameObject enemy;
        if (isChamp)
            enemy = Instantiate(_spawnPrefabsChamps[id], location, Quaternion.identity);
        else
            enemy = Instantiate(_spawnPrefabs[id], location, Quaternion.identity);

        if (_enemyTransform != null)
        {
            enemy.transform.SetParent(_enemyTransform);
        }

        Monster monsterScript = enemy.GetComponent<Monster>();
        if (monsterScript != null)
        {
            monsterScript.InitMonster(_playerTransform);
        }

        _enemies.Add(enemy);
    }

    public void SpawnEnemiesOfCurrentLevel()
    {
        if (!_playerFound)
        {
            FindPlayer();
        }

        for (int nrOfEnemies = 0; nrOfEnemies < _numberOfEnemiesPerSpawn; nrOfEnemies++)
        {
            for (int index = 0; index < _spawnPositions.Count; index++)
            {
                bool isChamp = false;
                int randomEnemy = Random.Range(0, _spawnPrefabs.Count);

                if (LevelManager.Instance.LevelNumber >= 3)
                {
                    int spawnChamp = Random.Range(0, 1);
                    if (spawnChamp == 0)
                    {
                        isChamp = true;
                        randomEnemy = Random.Range(0, _spawnPrefabsChamps.Count);
                    }
                }

                SpawnEnemy(randomEnemy, _spawnPositions[index].position, isChamp);
            }
        }
    }

    public void ClearEnemies()
    {
        foreach (GameObject go in _enemies)
        {
            Destroy(go);
        }
        _enemies.Clear();
    }
}
