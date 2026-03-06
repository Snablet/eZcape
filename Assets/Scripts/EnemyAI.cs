using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    private NavMeshAgent agent;

    [Header("Settings")]
    public float chaseRange = 10f;
    public float attackRange = 1.5f;  // How close to attack
    public float moveSpeed = 5f;

    [Header("Attack")]
    public int damageAmount = 10;
    public float attackCooldown = 1f;  // Time between attacks
    private float lastAttackTime = 0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
        agent.stoppingDistance = 0.5f;//attackRange * 0.8f;  // Stop a bit before attack range
        agent.autoBraking = false;

        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Chase if within range
        if (distanceToPlayer <= chaseRange)
        {
            agent.SetDestination(player.position);

            // Check if close enough to attack
            if (distanceToPlayer <= attackRange)
            {
                // Time to attack!
                if (Time.time > lastAttackTime + attackCooldown)
                {
                    Attack();
                    lastAttackTime = Time.time;
                }
            }
        }
        else
        {
            agent.ResetPath();
        }
    }

    void Attack()
    {
        Debug.Log("Enemy attacks!");

        // Find player and damage them
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damageAmount);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
