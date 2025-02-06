using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class LevelTimer : MonoBehaviour
{
    public float timeLimit = 300f; // 5 minutes in seconds
    public TextMeshProUGUI timerText; // Reference to the timer text
    public TextMeshProUGUI timesUpText; // Reference to the "Times Up" text
    public AudioSource SFX;
    public AudioSource loseSFX;
    public AudioClip alarmSFX;
    public AudioClip knockSFX;

    private float timeRemaining;
    public bool timerRunning = false;
    private bool playingAlarm = false;
    private bool playingKnock = false;

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
                if (timeRemaining <= 11)
                {
                    if (playingAlarm == false)
                    {
                        SFX.PlayOneShot(alarmSFX);
                        playingAlarm = true;
                    }
                    if (timeRemaining < 5)
                    {
                        if (playingKnock == false)
                        {
                            SFX.PlayOneShot(knockSFX);
                            playingKnock = true;
                        }
                    }
                    if (timeRemaining < 1)
                    {
                        SFX.Stop();
                        loseSFX.Play();
                    }
                }
            }
            else
            {
                playingAlarm = false;
                playingKnock = false;
                
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