using UnityEngine;

public class MedkitScript : MonoBehaviour
{
    public GameObject pickupEffect;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Pickup(other);
        }
    }

    void Pickup(Collider player)
    {
        Instantiate(pickupEffect, transform.position, transform.rotation);

        PlayerControl2 playerControl = player.GetComponent<PlayerControl2>();



        Destroy(gameObject);
    }
}
