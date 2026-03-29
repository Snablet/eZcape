using UnityEngine;
using TMPro;

public class TimerScript : MonoBehaviour
{
    public float remainingTime;
    public GameObject diedMenu;

    private bool hasEnded = false;

    TextMeshProUGUI timerText;

    void Start()
    {
        remainingTime = 60f;
        timerText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }
        else if (!hasEnded)
        {
            remainingTime = 0;
            hasEnded = true;
            diedMenu.SetActive(true);
            Time.timeScale = 0f; // freeze game
        }

        int minutes = Mathf.FloorToInt(remainingTime / 60f);
        int seconds = Mathf.FloorToInt(remainingTime % 60f);

        timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }
}