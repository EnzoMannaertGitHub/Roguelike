using UnityEngine;

public class DeathboxScript : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
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
