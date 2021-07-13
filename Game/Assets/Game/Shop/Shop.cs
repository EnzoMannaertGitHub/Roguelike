using System.Collections.Generic;
using UnityEngine;
public class Shop : MonoBehaviour
{
    [SerializeField] List<Transform> _spawnLocations;
    [SerializeField] GameObject _itemStand;
    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform t in _spawnLocations)
        {
            Vector3 pos = new Vector3(t.position.x, t.position.y, 0);
            Instantiate(_itemStand, pos, t.rotation);
        }
    }
}