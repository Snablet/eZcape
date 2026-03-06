using UnityEngine;

public class enemyHealth:MonoBehaviour
{
    [SerializeField] private float health;

    public void TakeDamage(float damage) 
    {
    health-=damage;
        Debug.Log(health);
    
    }
}
