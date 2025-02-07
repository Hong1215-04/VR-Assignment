using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameProgressManager : MonoBehaviour
{
    public static GameProgressManager Instance; // Singleton instance

    public Slider progressBar; // Reference to the UI progress bar
    public TextMeshProUGUI progressText; // Reference to the progress text
    public AudioSource taskIncreaseSFX; // Play sfx when task increase by 1

    private int totalStains; // Total number of stains in the scene
    private int totalTrash; // Total number of trash items in the scene
    private int totalCockroach; // Total number of cockroach items in the scene
    private int progressTrashUnits; // Number of trash units contributing to progress
    private int cleanedStains = 0; // Counter for cleaned stains
    private int cleanedTrash = 0; // Counter for cleaned trash
    private int cleanedCockroach = 0; // Counter for cleaned cockroach

    public delegate void ProgressUpdated(); // Delegate for progress update event
    public event ProgressUpdated OnProgressUpdated; // Event triggered on progress update

    public delegate void LevelCompleted(); // Delegate for level completion event
    public event LevelCompleted OnLevelCompleted; // Event triggered on level completion

    void Awake()
    {
        // Ensure only one instance exists (Singleton pattern)
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        InitializeStainCount(); // Initialize stain count at the start
        InitializeTrashCount(); // Initialize trash count at the start
        InitializeCockroachCount(); // Initialize cockroach count at the start
    }

    void Start()
    {
        InitializeProgressBar(); // Initialize the progress bar at the start
    }

    void InitializeStainCount()
    {
        // Find all game objects with the tag "Stain" and count them
        totalStains = GameObject.FindGameObjectsWithTag("Stain").Length;

        if (totalStains == 0)
        {
            Debug.LogWarning("No stains found in the scene! Check if stains have been instantiated.");
        }
    }

    void InitializeTrashCount()
    {
        // Find all game objects with the tag "Trash" and count them
        totalTrash = GameObject.FindGameObjectsWithTag("Trash").Length;
        if (totalTrash == 0)
        {
            Debug.LogWarning("No trash found in the scene! Check if trash has been instantiated.");
        }
        else
        {
            // Calculate the number of trash units contributing to progress
            progressTrashUnits = Mathf.CeilToInt((float)totalTrash / 10);
        }
    }

    void InitializeCockroachCount()
    {
        // Find all game objects with the tag "Cockroach" and count them
        totalCockroach = GameObject.FindGameObjectsWithTag("Cockroach").Length;
        if (totalCockroach == 0)
        {
            Debug.LogWarning("No cockroach found in the scene! Check if cockroach objects have been instantiated.");
        }
    }

    void InitializeProgressBar()
    {
        if (progressBar != null)
        {
            // Set progress bar max value to the total number of tasks (stains + progress trash units + cockroach)
            progressBar.maxValue = totalStains + progressTrashUnits + totalCockroach;
            progressBar.value = 0; // Initialize progress bar value to 0
        }

        if (progressText != null)
        {
            // Set initial progress text
            progressText.text = $"0/{totalStains + progressTrashUnits + totalCockroach}";
        }
    }

    public void CleanStain()
    {
        cleanedStains++; // Increment cleaned stains count
        UpdateProgress(); // Update the progress bar and text
    }

    public void CleanTrash()
    {
        // Handle trash cleaning via DetactTrash
        cleanedTrash++; // Increment cleaned trash count
        UpdateProgress(); // Update the progress bar and text
    }

    public void CleanCockroach()
    {
        cleanedCockroach++; // Increment cleaned cockroach count
        UpdateProgress(); // Update the progress bar and text
    }

    void UpdateProgress()
    {
        if (progressBar != null)
        {
            // Update progress bar value
            progressBar.value = cleanedStains + cleanedTrash + cleanedCockroach;
        }

        if (progressText != null)
        {
            // Check if all tasks are completed
            if (cleanedStains + cleanedTrash + cleanedCockroach == progressBar.maxValue)
            {
                Debug.Log("checked");
                progressText.text = "Level Completed"; // Display level completion text
                OnLevelCompleted?.Invoke(); // Trigger level completion event
            }
            else
            {
                // Update progress text with current counts
                progressText.text = $"{cleanedStains + cleanedTrash + cleanedCockroach}/{totalStains + progressTrashUnits + totalCockroach}";
                taskIncreaseSFX.Play();
            }
        }

        OnProgressUpdated?.Invoke(); // Trigger progress update event
    }

    public int GetCurrentProgress()
    {
        return cleanedStains + cleanedTrash + cleanedCockroach; // Return current progress
    }

    public int GetTotalTargets()
    {
        return totalStains + progressTrashUnits + totalCockroach; // Return total number of targets
    }
}