using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private Animator animator;//for animator
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
        animator = GetComponent<Animator>();//for zom animations
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
        agent.stoppingDistance = 0.5f;//attackRange * 0.8f;  // Stop a bit before attack range
        agent.autoBraking = false;

        if (player == null)
            player = GameObject.FindGameObjectWithTag("Survivalist").transform;
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
                //this agent.reset is so the zombie stop moving when attack
                agent.ResetPath();
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
        RotateTowardsPlayer();
        //This is for animation parameters/ smooth transitions(0.1f,deltaTime)
        float zMoveAmount = agent.velocity.magnitude;
        animator.SetFloat("zMoveAmount", zMoveAmount, 0.1f, Time.deltaTime);
    }
    void RotateTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0; // keep zombie upright
        if (direction.magnitude > 0.1f)
        {
            float rotateSpeed = 5f; // change for faster/slower rotation
            Vector3 newDir = Vector3.RotateTowards(transform.forward, direction, rotateSpeed * Time.deltaTime, 0f);
            transform.rotation = Quaternion.LookRotation(newDir);
        }
    }

    void Attack()
    {
        Debug.Log("Enemy attacks!");
        //this is for zom attack animation
        animator.ResetTrigger("zomAttack");
        animator.SetTrigger("zomAttack");
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
