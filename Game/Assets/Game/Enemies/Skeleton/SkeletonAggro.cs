using UnityEngine;

public class SkeletonAggro : MonoBehaviour
{
    [SerializeField] private Skeleton _skeleton = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnTriggerStay2D(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.gameObject.layer != 11)
        {
            if (_skeleton != null)
                _skeleton.TriggerAttack();
        }
    }
}
