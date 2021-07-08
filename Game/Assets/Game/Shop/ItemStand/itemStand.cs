using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class itemStand : MonoBehaviour
{
    private int _cost = 1;
    private bool _isActive = true;
    [SerializeField] List<pickup> _items;
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
                _items[0].enabled = true;

                _isActive = false;
            }
        }
    }
}
