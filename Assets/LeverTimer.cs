using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class LevelTimer : MonoBehaviour
{
    public float timeLimit = 300f; // 5 minutes in seconds
    public TextMeshProUGUI timerText; // Reference to the timer text
    public TextMeshProUGUI timesUpText; // Reference to the "Times Up" text

    private float timeRemaining;
    public bool timerRunning = false;

    [SerializeField] private scene_manager sm;

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
                UpdateTimerText();
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                timerRunning = false;
                timeRemaining = 0f;
                UpdateTimerText();
                              
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
        timesUpText.text = "TimesUp";

        StartCoroutine(LoadNextScene());
    }

    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(sm.getLevelBetweenDuration());
        sm.setNextCondition("Next");
    }
}