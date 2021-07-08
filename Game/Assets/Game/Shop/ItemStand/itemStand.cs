using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class itemStand : MonoBehaviour
{
    [SerializeField] Text _costText;

    private int _cost = 1;
    private bool _isActive = true;
    [SerializeField] private List<GameObject> _items;
    [SerializeField] private Transform _itemLocation;
    private GameObject _item;
    private void Start()
    {
        if (_cost == 0)
            _costText.text = "";
        else
            _costText.text = $"{_cost}";

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
