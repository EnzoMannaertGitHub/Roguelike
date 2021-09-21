using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
public class itemStand : MonoBehaviour
{
    [SerializeField] private TextMeshPro _costText;
    [SerializeField] private SpriteRenderer _eKey;
    [SerializeField] private List<GameObject> _items;
    [SerializeField] private Transform _itemLocation;
    [SerializeField] private ParticleSystem _particles;

    private Transform _playerTransform;
    private int _cost;
    private bool _isActive = true;
    private GameObject _item;
    private float _range = 1.5f;
    private int _Stage = 1;
    private void Start()
    {
        _playerTransform = FindObjectOfType<PlayerMovement>().gameObject.transform;
    }
    private void Update()
    {
        if (!_isActive || _playerTransform == null)
            return;

        float distance = Vector2.Distance(transform.position, _playerTransform.position);
        Color c = _eKey.color;

        if (distance > _range)
        {
            _eKey.color = new Color(c.r, c.g, c.b, 0);
            _costText.alpha = 0;
        }
        else
        {
            float alpha = _range - distance;
            _eKey.color = new Color(c.r, c.g, c.b, alpha);
            _costText.alpha = alpha;
        }
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
                wallet.AddCash(-_cost);
                _item.GetComponent<pickup>().enabled = true;
                _item.GetComponent<pickup>().Interact();
                _particles.Emit(10);
                _costText.text = "";
                _eKey.forceRenderingOff = true;
                _isActive = false;
            }
        }
    }

    public void Reload(bool IsActive)
    {
        _isActive = IsActive;
        Destroy(_item);
        Init();
    }

    private void Init()
    {
        if (!_isActive)
        {
            if (_item != null)
                _item.SetActive(false);
            return;
        }
        _Stage = LevelManager.Instance.LevelNumber;
        _cost = Random.Range(14, 36 * _Stage);

        _eKey.forceRenderingOff = false;

        if (_cost == 14)
            _costText.text = "FREE";
        else
            _costText.text = $"{_cost} $";

        int index = Random.Range(0, _items.Count);

        var c = _eKey.color;
        _eKey.color = new Color(c.r, c.g, c.b, 255);

        _item = Instantiate(_items[index], _itemLocation.position, _itemLocation.rotation);
        _item.SetActive(_isActive);
    }
}
