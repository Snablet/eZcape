using UnityEngine;

public class FinishRoomScript : MonoBehaviour
{
    public GameObject endMenu;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            endMenu.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
