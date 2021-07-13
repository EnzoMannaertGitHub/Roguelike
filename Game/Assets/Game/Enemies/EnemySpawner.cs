using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _spawnPositions;
    private void Start()
    {
        SpawnEnemiesOfCurrentLevel();
    }

    List<GameObject> _enemies = new List<GameObject>();
    [SerializeField] private Transform _enemyTransform = null;

    [SerializeField] private List<GameObject> _spawnPrefabs = new List<GameObject>();          // Prefabs of all non-champ enemies
    [SerializeField] private List<GameObject> _spawnPrefabsChamps = new List<GameObject>();   // Prefabs of all champ enemies

    [SerializeField] private List<GameObject> _breedObjects = new List<GameObject>();          // Breed GameObjects (MUST BE SAME ORDER)
    [SerializeField] private List<GameObject> _breedObjectsChamps = new List<GameObject>();   // Breed GameObjects of champs (MUST BE SAME ORDER)

    private Transform _playerTransform = null;
    private PlayerMovement _playerMovement = null;
    private bool _playerFound = false;
    private int _numberOfEnemies = 3;

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
            if (isChamp)
                monsterScript.InitMonster(_breedObjectsChamps[id].GetComponent<Breed>(), _playerTransform);
            else
                monsterScript.InitMonster(_breedObjects[id].GetComponent<Breed>(), _playerTransform);
        }

        EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.SetPlayerMovement(_playerMovement);
        }

        _enemies.Add(enemy);
    }

    private void SpawnEnemiesOfCurrentLevel()
    {
        if (!_playerFound)
        {
            FindPlayer();
        }
        
        for (int index = 0; index < _numberOfEnemies; index++)
        {
            bool isChamp = false;
            int randomIndex = Random.Range(0, _spawnPositions.Count);
            int randomEnemy = Random.Range(0, _spawnPrefabs.Count);

            if (LevelManager.Instance.LevelNumber >= 5)
            {
                int spawnChamp = Random.Range(0, 1);
                if (spawnChamp == 0)
                {
                    Debug.Log("Spawning champ");
                    isChamp = true;
                    randomEnemy = Random.Range(0, _spawnPrefabsChamps.Count);
                }
            }

            SpawnEnemy(randomEnemy, _spawnPositions[randomIndex].position, isChamp);
        }
    }

    public void ClearEnemies()
    {
        foreach(GameObject go in _enemies)
        {
            Destroy(go);
        }
        _enemies.Clear();
    }
}