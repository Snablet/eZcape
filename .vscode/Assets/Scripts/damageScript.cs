using UnityEngine;

public class damageScript:MonoBehaviour
{
    
    [SerializeField] private float damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("enemy")) 
        {
            enemyHealth enemy = other.GetComponent<enemyHealth>();
            enemy.TakeDamage(damage);
        }
    }

}
