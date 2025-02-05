using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class LevelProgressBar : MonoBehaviour
{
    public ParticleSystem Confetti_right;
    public ParticleSystem Confetti_left;
    public LevelTimer timer;
    public AudioSource victory;
    public AudioSource pop;

    private Slider progressBar; // Reference to the slider component
    private TextMeshProUGUI progressText; // Reference to the TextMeshPro component
    [SerializeField] private scene_manager sm;

    void Start()
    {
        // Get the slider component attached to the same GameObject
        progressBar = GetComponent<Slider>();
        // Get the TextMeshPro component from the child GameObject
        progressText = GetComponentInChildren<TextMeshProUGUI>();

        if (GameProgressManager.Instance != null)
        {
            // Subscribe to progress update and level completion events
            GameProgressManager.Instance.OnProgressUpdated += UpdateProgress;
            GameProgressManager.Instance.OnLevelCompleted += ShowLevelCompleted;
            InitializeProgressBar(); // Initialize the progress bar at the start
        }
        else
        {
            Debug.LogError("GameProgressManager not found in the scene!");
        }
        Confetti_right.Stop();
        Confetti_left.Stop();
    }

    void InitializeProgressBar()
    {
        if (GameProgressManager.Instance != null)
        {
            // Set progress bar max value and current value
            progressBar.maxValue = GameProgressManager.Instance.GetTotalTargets();
            progressBar.value = GameProgressManager.Instance.GetCurrentProgress();
            // Set initial progress text
            progressText.text = $"{GameProgressManager.Instance.GetCurrentProgress()}/{GameProgressManager.Instance.GetTotalTargets()}";
        }
    }

    void UpdateProgress()
    {
        if (progressBar != null && GameProgressManager.Instance != null)
        {
            // Update progress bar value
            progressBar.value = GameProgressManager.Instance.GetCurrentProgress();
            // Update progress text with current counts
            progressText.text = $"{GameProgressManager.Instance.GetCurrentProgress()}/{GameProgressManager.Instance.GetTotalTargets()}";
        }
    }

    void ShowLevelCompleted()
    {
        // Display level completion text
        timer.timesUpText.gameObject.SetActive(true);
        timer.timesUpText.text = "Level Completed";
        timer.timerRunning = false;
        pop.Play();
        Confetti_right.Play();
        Confetti_left.Play();
        victory.Play();

        StartCoroutine(LoadNextScene());
    }

    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(sm.getLevelBetweenDuration());
        Confetti_right.Stop();
        Confetti_left.Stop();
        sm.setNextCondition("Next");  
    }
}