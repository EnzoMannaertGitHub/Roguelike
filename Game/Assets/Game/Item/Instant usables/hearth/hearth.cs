using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hearth : MonoBehaviour
{
    private void Start()
    {
        Vector3 scale = (GetComponentInChildren<SpriteRenderer>().transform.localScale / 4f);
        GetComponentInChildren<SpriteRenderer>().transform.localScale = scale;

        Vector2 colliderPos = GetComponent<BoxCollider2D>().transform.position;
        Vector2 colliderSize = GetComponent<BoxCollider2D>().bounds.extents;

        //set sprite to collider Pos with extra height
        Vector3 spritePos = GetComponentInChildren<SpriteRenderer>().transform.position;
        GetComponentInChildren<SpriteRenderer>().transform.position = colliderPos - colliderSize;
        GetComponentInChildren<SpriteRenderer>().transform.position += new Vector3 (0,spritePos.y / 3.5f, 0);
    }
    [SerializeField] private LayerMask _layermask;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.gameObject.layer != 8)
        {
            collision.GetComponent<Health>().ResetHealthToMax();
            Destroy(gameObject);
        }
    }
}
