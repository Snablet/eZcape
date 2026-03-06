using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 100;
    public int currentHealth;

    [Header("Damage Settings")]
    public int damageAmount = 10; // How much damage per hit
    public float damageInterval = 0.5f; // Time between damage ticks while touching enemy

    [Header("Invincibility")]
    public float invincibilityTime = 1f; // How long invincible after hit
    private bool isInvincible = false;

    // For continuous damage
    private bool isTouchingEnemy = false;
    private GameObject currentEnemy;
    private float nextDamageTime = 0f;

    void Start()
    {
        currentHealth = maxHealth;
        Debug.Log($"Player starting health: {currentHealth}");
    }

    void Update()
    {
        // Handle continuous damage while touching enemy
        if (isTouchingEnemy && !isInvincible && Time.time >= nextDamageTime)
        {
            TakeDamage(damageAmount);
            nextDamageTime = Time.time + damageInterval;
        }
    }

    public void TakeDamage(int damage)
    {
        if (isInvincible) return; // Skip damage if invincible

        currentHealth -= damage;
        Debug.Log($"Player took {damage} damage! Health: {currentHealth}");

        // Start invincibility
        StartCoroutine(InvincibilityFrames());

        // Visual feedback (optional)
        StartCoroutine(FlashRed());

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    IEnumerator InvincibilityFrames()
    {
        isInvincible = true;
        Debug.Log("Invincibility ON");

        // Optional: Make player blink
        Renderer playerRenderer = GetComponent<Renderer>();
        Color originalColor = playerRenderer.material.color;

        for (float i = 0; i < invincibilityTime; i += 0.1f)
        {
            if (playerRenderer != null)
                playerRenderer.material.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            if (playerRenderer != null)
                playerRenderer.material.color = originalColor;
            yield return new WaitForSeconds(0.1f);
        }

        isInvincible = false;
        Debug.Log("Invincibility OFF");
    }

    IEnumerator FlashRed()
    {
        Renderer playerRenderer = GetComponent<Renderer>();
        if (playerRenderer != null)
        {
            Color originalColor = playerRenderer.material.color;
            playerRenderer.material.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            playerRenderer.material.color = originalColor;
        }
    }

    void Die()
    {
        Debug.Log("Player died!");
        // Add death effects here (respawn, game over, etc.)
    }

    // Called when touching the enemy
    public void StartTouchingEnemy(GameObject enemy)
    {
        isTouchingEnemy = true;
        currentEnemy = enemy;
        nextDamageTime = Time.time; // Allow immediate damage
        Debug.Log("Started touching enemy");
    }

    // Called when leaving the enemy
    public void StopTouchingEnemy()
    {
        isTouchingEnemy = false;
        currentEnemy = null;
        Debug.Log("Stopped touching enemy");
    }

    // Simple health display
    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 200, 20), $"Health: {currentHealth}");
        if (isInvincible)
            GUI.Label(new Rect(10, 30, 200, 20), "INVINCIBLE!");
    }
}
