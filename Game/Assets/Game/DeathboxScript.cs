using UnityEngine;

public class DeathboxScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject other = collision.gameObject;
        if (other.CompareTag("Player"))
        {
            Health playerHealth = other.GetComponent<Health>();

            if (playerHealth != null)
            {
                playerHealth.KillPlayer();
            }
        }
    }
}