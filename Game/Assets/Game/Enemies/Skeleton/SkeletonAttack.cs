using UnityEngine;

public class SkeletonAttack : MonoBehaviour
{
    [SerializeField] private Skeleton _skeleton = null;

    private Collider2D playerCollider = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnTriggerStay2D(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerCollider = collision;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerCollider = null;
        }
    }

    public void CheckPlayerHit()
    {
        if (playerCollider != null)
        {
            Health playerHealth = playerCollider.GetComponent<Health>();
            if (playerHealth == null)
                return;

            Vector2 dir = new Vector2();
            if (transform.position.x <= _skeleton.GetPlayerTransform().position.x)
                dir.x = 1;
            else
                dir.x = -1;

            if (transform.position.y <= _skeleton.GetPlayerTransform().position.y)
                dir.y = 1;
            else
                dir.y = -1;

            if (_skeleton != null)
                playerHealth.GetHit(_skeleton.GetDamage(), dir);
        }
    }
}
