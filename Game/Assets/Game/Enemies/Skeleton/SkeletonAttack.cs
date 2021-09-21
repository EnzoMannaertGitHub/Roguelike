using UnityEngine;

public class SkeletonAttack : MonoBehaviour
{
    [SerializeField] private Skeleton _skeleton = null;
    [SerializeField] private Transform _particleTransform = null;
    [SerializeField] private ParticleSystem _particleSystem = null;

    private Collider2D playerCollider = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnTriggerStay2D(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.gameObject.layer != 11)
        {
            playerCollider = collision;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.gameObject.layer != 11)
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

            Transform playerTransform = _skeleton.GetPlayerTransform();
            _particleTransform.position = playerTransform.position;
            _particleSystem.Emit(15);

            Vector2 dir = new Vector2();
            if (transform.position.x <= playerTransform.position.x)
                dir.x = 1;
            else
                dir.x = -1;

            if (transform.position.y <= playerTransform.position.y)
                dir.y = 1;
            else
                dir.y = -1;

            if (_skeleton != null)
                playerHealth.GetHit(_skeleton.GetDamage(), dir);
        }
    }
}
