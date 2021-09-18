using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hearth : MonoBehaviour
{
    private void Start()
    {
      
    }
    [SerializeField] private LayerMask _layermask;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("hearth collided");
        if (collision.CompareTag("Player") && collision.gameObject.layer != 8)
        {
            collision.GetComponent<Health>().ResetHealthToMax();
            Destroy(gameObject);
        }
    }
}
