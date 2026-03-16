using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public float startTime;
    TextMeshProUGUI timerText;

    void Start()
    {
        startTime = Time.time;
        timerText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        float currentTime = Time.time - startTime;

        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);

        timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }
}
