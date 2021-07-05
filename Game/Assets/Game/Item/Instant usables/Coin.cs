using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int _value = 1;
    [SerializeField] LayerMask _layermask;
    private Rigidbody2D _rigidBody;
    private void Awake()
    {
        _rigidBody = gameObject.GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Wallet>().Total += _value;
            Destroy(gameObject);
        }
    }
}
