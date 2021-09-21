using UnityEngine;

public class SkeletonVision : MonoBehaviour
{
    [SerializeField] private Skeleton _skeleton = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnTriggerStay2D(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.gameObject.layer != 11 && _skeleton != null)
        {
            _skeleton._playerSeen = true;
            _skeleton.StartCharge();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.gameObject.layer != 11)
        {
            _skeleton._playerSeen = false;
            _skeleton.StopCharge();
        }
    }
}
