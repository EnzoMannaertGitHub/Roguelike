using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
public class itemStand : MonoBehaviour
{
    [SerializeField] private TextMeshPro _costText;
    [SerializeField] private List<GameObject> _items;
    [SerializeField] private Transform _itemLocation;

    private Transform _playerTransform;
    private int _cost;
    private bool _isActive = true;
    private GameObject _item;
    private float _range = 1.5f;
    private int _Stage = 1;
    private void Start()
    {
        _cost = Random.Range(0, 5 * _Stage);

        _playerTransform = FindObjectOfType<PlayerMovement>().gameObject.transform;

        if (_cost == 0)
            _costText.text = "";
        else
            _costText.text = $"{_cost} $";

        int index = Random.Range(0, _items.Count);
        _item = Instantiate(_items[index], _itemLocation.position, _itemLocation.rotation);
    }
    private void Update()
    {
        float distance = Vector2.Distance(transform.position, _playerTransform.position);
        if (distance > _range)
            _costText.alpha = 0;
        else
            _costText.alpha = _range - distance;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!_isActive)
            return;
        
        if (collision.CompareTag("Player") && Input.GetAxis("Interact") > 0.5f)
        {
            Wallet wallet = collision.gameObject.GetComponent<Wallet>();
            if (wallet.Total >= _cost)
            {
                wallet.Total -= _cost;
                _item.GetComponent<pickup>().enabled = true;
                _costText.text = "";

                _isActive = false;
            }
        }
    }
}
