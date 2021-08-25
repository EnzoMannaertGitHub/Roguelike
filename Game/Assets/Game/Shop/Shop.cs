using System.Collections.Generic;
using UnityEngine;
public class Shop : MonoBehaviour
{
    [SerializeField] private List<Transform> _spawnLocations;
    [SerializeField] private GameObject _itemStand;
    [SerializeField] private GameObject _shopSpawn;
    [SerializeField] private upgradeSHop _upgradeStand;
    private int _numberOfItems = 2;
    private GameObject _player;
    List<GameObject> _itemStands = new List<GameObject>();
    private bool _isInShop = false;
    public bool IsInShop { get { return _isInShop; } set { _isInShop = value; } }
    // Start is called before the first frame update
    private void Start()
    {
        InstantiateItemStands();
        int item = 0;
        foreach(var i in _itemStands)
        {
            if (item < _numberOfItems)
            {
                i.SetActive(true);
                i.GetComponentInChildren<itemStand>().Reload(true);
            }
            else
            {
                i.GetComponentInChildren<itemStand>().Reload(false);
            }
            item++;
        }

        _player = FindObjectOfType<PlayerMovement>().gameObject;
    }

    private void Update()
    {
        if (_isInShop)
        {
            if (_player.transform.position.y < _shopSpawn.transform.position.y - 1.5f)
            {
                _player.transform.position = _shopSpawn.transform.position;
            }
        }
    }

    public void ReloadShop()
    {
        ClearItemStands();
        _upgradeStand.SetSctive();
    }

    public void UpgradeShop()
    {
        Debug.Log("dfd");
        _numberOfItems++;
        ClearItemStands();
    }
    
    private void ClearItemStands()
    {
        int item = 0;
        foreach (var i in _itemStands)
        {
            if (item < _numberOfItems)
            {
                i.SetActive(true);
                i.GetComponentInChildren<itemStand>().Reload(true);
            }
            else
            {
                i.GetComponentInChildren<itemStand>().Reload(false);
            }
            item++;
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