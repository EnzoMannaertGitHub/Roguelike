using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class itemStand : MonoBehaviour
{
    private int _cost = 1;
    private bool _isActive = true;
    [SerializeField] private List<GameObject> _items;
    [SerializeField] private Transform _itemLocation;
    private GameObject _item;
    private void Start()
    {
        int index = Random.Range(0, _items.Count);
        _item = Instantiate(_items[index], _itemLocation.position, _itemLocation.rotation);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_isActive)
            return;
        
        if (collision.CompareTag("Player"))
        {
            Wallet wallet = collision.gameObject.GetComponent<Wallet>();
            if (wallet.Total >= _cost)
            {
                wallet.Total -= _cost;
                _item.GetComponent<pickup>().enabled = true;

                _isActive = false;
            }
        }
    }
}
