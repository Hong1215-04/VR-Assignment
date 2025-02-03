using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameProgressManager : MonoBehaviour
{
    public static GameProgressManager Instance; // Singleton instance

    public Slider progressBar; // Reference to the UI progress bar
    public TextMeshProUGUI progressText; // Reference to the progress text

    private int totalStains; // Total number of stains in the scene
    private int totalTrash; // Total number of trash items in the scene
    private int totalAI; // Total number of AI items in the scene
    private int progressTrashUnits; // Number of trash units contributing to progress
    private int cleanedStains = 0; // Counter for cleaned stains
    private int cleanedTrash = 0; // Counter for cleaned trash
    private int cleanedAI = 0; // Counter for cleaned AI

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
        InitializeAICount(); // Initialize AI count at the start
    }

    void Start()
    {
        InitializeProgressBar(); // Initialize the progress bar at the start
    }

    void InitializeStainCount()
    {
        // Find all game objects with the tag "Stain" and count them
        totalStains = 1;  //GameObject.FindGameObjectsWithTag("Stain").Length;

        if (totalStains == 0)
        {
            Debug.LogWarning("No stains found in the scene! Check if stains have been instantiated.");
        }
    }

    void InitializeTrashCount()
    {
        // Find all game objects with the tag "Trash" and count them
        totalTrash = 0; //GameObject.FindGameObjectsWithTag("Trash").Length;
        if (totalTrash == 0)
        {
            Debug.LogWarning("No trash found in the scene! Check if trash has been instantiated.");
        }
        else
        {
            // Calculate the number of trash units contributing to progress
            progressTrashUnits = Mathf.CeilToInt((float)totalTrash / 5);
        }
    }

    void InitializeAICount()
    {
        // Find all game objects with the tag "AI" and count them
        totalAI = 0; //GameObject.FindGameObjectsWithTag("AI").Length;
        if (totalAI == 0)
        {
            Debug.LogWarning("No AI found in the scene! Check if AI objects have been instantiated.");
        }
    }

    void InitializeProgressBar()
    {
        if (progressBar != null)
        {
            // Set progress bar max value to the total number of tasks (stains + progress trash units + AI)
            progressBar.maxValue = totalStains + progressTrashUnits + totalAI;
            progressBar.value = 0; // Initialize progress bar value to 0
        }

        if (progressText != null)
        {
            // Set initial progress text
            progressText.text = $"0/{totalStains + progressTrashUnits + totalAI}";
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

    public void CleanAI()
    {
        cleanedAI++; // Increment cleaned AI count
        UpdateProgress(); // Update the progress bar and text
    }

    void UpdateProgress()
    {
        if (progressBar != null)
        {
            // Update progress bar value
            progressBar.value = cleanedStains + cleanedTrash + cleanedAI;
        }

        if (progressText != null)
        {
            // Check if all tasks are completed
            if (cleanedStains + cleanedTrash + cleanedAI == progressBar.maxValue)
            {
                Debug.Log("checked");
                progressText.text = "Level Completed"; // Display level completion text
                OnLevelCompleted?.Invoke(); // Trigger level completion event
            }
            else
            {
                // Update progress text with current counts
                progressText.text = $"{cleanedStains + cleanedTrash + cleanedAI}/{totalStains + progressTrashUnits + totalAI}";
            }
        }

        OnProgressUpdated?.Invoke(); // Trigger progress update event
    }

    public int GetCurrentProgress()
    {
        return cleanedStains + cleanedTrash + cleanedAI; // Return current progress
    }

    public int GetTotalTargets()
    {
        return totalStains + progressTrashUnits + totalAI; // Return total number of targets
    }
}