using UnityEngine;

public class BatProjectile : MonoBehaviour
{
    [SerializeField] private float _speed = 2.5f;
    public void shoot(Vector2 direction)
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = direction * _speed;
    }
}