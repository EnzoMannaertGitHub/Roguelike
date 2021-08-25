using UnityEngine;

public class SkeletonOverlap : MonoBehaviour
{
    [SerializeField] private Skeleton _skeleton = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnTriggerStay2D(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Health playerHealth = collision.GetComponent<Health>();
            if (playerHealth == null)
                return;

            _skeleton.FacePlayer();

            Vector2 dir = new Vector2();
            if (transform.position.x <= _skeleton.GetPlayerTransform().position.x)
                dir.x = 1;
            else
                dir.x = -1;

            if (transform.position.y <= _skeleton.GetPlayerTransform().position.y)
                dir.y = 1;
            else
                dir.y = -1;

            playerHealth.GetHit(_skeleton.GetDamage(), dir);
        }
    }
}
