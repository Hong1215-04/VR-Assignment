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
    private int cleanedStains = 0; // Counter for cleaned stains
    private int cleanedTrash = 0; // Counter for cleaned trash

    private int trashBinCapacity = 5; // Capacity of the trash bin
    private int currentTrashInBin = 0; // Current number of trash items in the bin

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
    }

    void InitializeProgressBar()
    {
        if (progressBar != null)
        {
            // Set progress bar max value to the total number of tasks (stains + trash)
            progressBar.maxValue = totalStains + totalTrash;
            progressBar.value = 0; // Initialize progress bar value to 0
        }

        if (progressText != null)
        {
            // Set initial progress text
            progressText.text = $"0/{totalStains + totalTrash}";
        }
    }

    public void CleanStain()
    {
        cleanedStains++; // Increment cleaned stains count
        UpdateProgress(); // Update the progress bar and text
    }

    public void CleanTrash()
    {
        cleanedTrash++; // Increment cleaned trash count
        currentTrashInBin++; // Increment current trash in bin

        // Check if trash bin is full
        if (currentTrashInBin >= trashBinCapacity)
        {
            CreateTrashBag(); // Create a new trash bag
            currentTrashInBin = 0; // Reset trash bin count
        }

        UpdateProgress(); // Update the progress bar and text
    }

    void CreateTrashBag()
    {
        // Logic for creating a trash bag
        Debug.Log("Trash bin is full! Creating a trash bag.");
        totalTrash++; // Increase total trash count to account for the new trash bag
        UpdateProgress(); // Update the progress bar and text
    }

    void UpdateProgress()
    {
        if (progressBar != null)
        {
            // Update progress bar value
            progressBar.value = cleanedStains + cleanedTrash;
        }

        if (progressText != null)
        {
            // Check if all tasks are completed
            if (cleanedStains + cleanedTrash == progressBar.maxValue)
            {
                progressText.text = "Level Completed"; // Display level completion text
                OnLevelCompleted?.Invoke(); // Trigger level completion event
            }
            else
            {
                // Update progress text with current counts
                progressText.text = $"{cleanedStains + cleanedTrash}/{totalStains + totalTrash}";
            }
        }

        OnProgressUpdated?.Invoke(); // Trigger progress update event
    }

    public int GetCurrentProgress()
    {
        return cleanedStains + cleanedTrash; // Return current progress
    }

    public int GetTotalTargets()
    {
        return totalStains + totalTrash; // Return total number of targets
    }
}