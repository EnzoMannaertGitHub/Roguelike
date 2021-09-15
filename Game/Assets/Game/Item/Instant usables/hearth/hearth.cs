using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hearth : MonoBehaviour
{
    [SerializeField] private LayerMask _layermask;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Health>().ResetHealthToMax();
            Destroy(gameObject);
        }
    }
}
