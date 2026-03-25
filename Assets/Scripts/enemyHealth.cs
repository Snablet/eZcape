using UnityEngine;
using System.Collections;

public class enemyHealth : MonoBehaviour
{
    [SerializeField] private float health = 100f;
    [SerializeField] private float staggerDuration = 0.8f;

    private Animator animator;
    private EnemyAI ai;

    private Coroutine staggerRoutine;

    void Awake()
    {
        animator = GetComponent<Animator>();
        ai = GetComponent<EnemyAI>();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log("Health: " + health);

        if (health <= 0)
        {
            Die();
            return;
        }

        //  stagger on hit
        if (staggerRoutine != null)
        {
            StopCoroutine(staggerRoutine);
        }

        staggerRoutine = StartCoroutine(Stagger());
    }

    IEnumerator Stagger()
    {
        // Reset trigger 
        animator.ResetTrigger("zstagger1");
        animator.SetTrigger("zstagger1");

        // Pause AI
        if (ai != null)
            ai.isStagger = true;

        yield return new WaitForSeconds(staggerDuration);

        // Resume AI
        if (ai != null)
            ai.isStagger = false;

        staggerRoutine = null;
    }

    void Die()
    {
        if (ai != null)
            ai.isStagger = true;

        animator.SetTrigger("zomDeath");

        Destroy(gameObject, 3f);
    }
}