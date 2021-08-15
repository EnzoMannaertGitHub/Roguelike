using System.Collections.Generic;
using UnityEngine;

public class Island : MonoBehaviour
{
    [SerializeField] private List<GameObject> _lootSpawns;
    public List<GameObject> LootSpawns { get { return _lootSpawns; } }

    [SerializeField] private List<GameObject> _enemySpawns;
    public List<GameObject> EnemySpawns { get { return _enemySpawns; } }
}