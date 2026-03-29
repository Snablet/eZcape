using UnityEngine;

public class SyringeScript : MonoBehaviour
{
    public GameObject pickupEffect;
    private TimerScript gameTimer;

    void Start()
    {
        gameTimer = FindFirstObjectByType<TimerScript>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Picked up");

            Instantiate(pickupEffect, transform.position, transform.rotation);

            if (gameTimer != null)
            {
                gameTimer.remainingTime += 60f;
            }

            Destroy(gameObject);
        }
    }
}