using System.Collections.Generic;
using UnityEngine;
public class Shop : MonoBehaviour
{
    [SerializeField] private List<Transform> _spawnLocations;
    [SerializeField] private GameObject _itemStand;
    [SerializeField] private GameObject _shopSpawn;
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
            if (item >= _numberOfItems)
                i.SetActive(false);
            else
                i.SetActive(true);
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
    }

    public void UpgradeShop()
    {
        _numberOfItems++;
        int item = 0;
        foreach (var i in _itemStands)
        {
            if (item >= _numberOfItems)
                i.SetActive(false);
            else
                i.SetActive(true);
            item++;
        }
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