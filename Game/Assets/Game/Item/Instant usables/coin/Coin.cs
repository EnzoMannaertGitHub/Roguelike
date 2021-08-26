using UnityEngine;
public class Coin : MonoBehaviour
{
    [SerializeField] private int _value = 1;
    [SerializeField] private LayerMask _layermask;
    [SerializeField] private GameObject _pickupAnimation;
    private Rigidbody2D _rigidBody;
    private void Awake()
    {
        _rigidBody = gameObject.GetComponent<Rigidbody2D>();
        _rigidBody.gravityScale = Random.Range(0.15f, 0.30f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Wallet>().AddCash(_value);
            Vector3 pos = transform.position;
            Instantiate(_pickupAnimation, new Vector3(pos.x, pos.y + 0.1f, 0), transform.rotation);
            Destroy(gameObject);
        }
    }
}
