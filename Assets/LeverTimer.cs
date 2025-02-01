using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelTimer : MonoBehaviour
{
    public float timeLimit = 300f; // 5 minutes in seconds
    public TextMeshProUGUI timerText; // Reference to the timer text
    public TextMeshProUGUI timesUpText; // Reference to the "Times Up" text

    private float timeRemaining;
    private bool timerRunning = false;

    void Start()
    {
        timeRemaining = timeLimit;
        timerRunning = true;
        timesUpText.gameObject.SetActive(false); // Hide "Times Up" text at start
    }

    void Update()
    {
        if (timerRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerText();
            }
            else
            {
                timeRemaining = 0;
                timerRunning = false;
                TimesUp();
            }
        }
    }

    void UpdateTimerText()
    {
        // Convert timeRemaining to minutes and seconds
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void TimesUp()
    {
        timesUpText.gameObject.SetActive(true); // Show "Times Up" text
        Debug.Log("Times Up!");
    }
}