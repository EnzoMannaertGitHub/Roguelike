using UnityEngine;
using TMPro;
public class upgradeSHop : MonoBehaviour
{
    [SerializeField] private TextMeshPro _costText;
    [SerializeField] private SpriteRenderer _eKey;
    [SerializeField] private Shop _shop;
    private Transform _playerTransform;
    private int _cost;
    private bool _isActive = true;
    private float _range = 1.5f;
    private void Start()
    {
        _playerTransform = FindObjectOfType<PlayerMovement>().gameObject.transform;
        Init();
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
                wallet.Total -= _cost;
                _costText.text = "";
                _eKey.forceRenderingOff = true;
                _isActive = false;
                _cost *= 2;
                _shop.UpgradeShop();
            }
        }
    }

    private void Init()
    {
        _cost = 5;

        _costText.text = $"Upgrade {_cost} $";
    }

    public void SetSctive()
    {
        if (_cost > 20)
            return;
        _isActive = true;
        _costText.text = $"Upgrade {_cost} $";
        _eKey.forceRenderingOff = false;
    }
}
