using UnityEngine;

public class BatProjectile : MonoBehaviour
{
    [SerializeField] private float _speed = 2.5f;
    public void shoot(Vector2 direction)
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = direction * _speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Health>().GetHit(5, gameObject.GetComponent<Rigidbody2D>().velocity);
            Destroy(gameObject);
        }
        if (other.gameObject.layer != 7 && other.gameObject.layer != 11)
        {
            Destroy(gameObject);
        }
    }
}