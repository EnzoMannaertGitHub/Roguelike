using System.Collections.Generic;
using UnityEngine;
public class Shop : MonoBehaviour
{
    [SerializeField] List<Transform> _spawnLocations;
    [SerializeField] GameObject _itemStand;
    List<GameObject> _itemStands = new List<GameObject>();
    // Start is called before the first frame update
    private void Start()
    {
        InstantiateItemStands();
    }

    public void ReloadShop()
    {
        ClearItemStands();
    }
    
    private void ClearItemStands()
    {
        foreach (GameObject i in _itemStands)
        {
            i.GetComponentInChildren<itemStand>().Reload();
        }
    }

    private void InstantiateItemStands()
    {
        foreach (Transform t in _spawnLocations)
        {
            Vector3 pos = new Vector3(t.position.x, t.position.y, 0);
            _itemStands.Add(Instantiate(_itemStand, pos, t.rotation));
        }
    }
}