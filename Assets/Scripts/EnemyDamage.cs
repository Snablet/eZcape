using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [Header("Damage Settings")]
    public int damageAmount = 10;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                // Tell player it's touching the enemy
                playerHealth.StartTouchingEnemy(gameObject);
            }
        }
    }

    void OnCollisionStay(Collision collision)
    {
        // This is called every frame while touching
        // The continuous damage is now handled in PlayerHealth
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                // Tell player it's no longer touching the enemy
                playerHealth.StopTouchingEnemy();
            }
        }
    }
}
