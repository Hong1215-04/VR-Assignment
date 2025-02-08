using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject settingsMenuUI;
    public InputActionReference menuButton;
    public GameObject playerMovement;

    private bool isPaused = false;

    void Start()
    {
        //InitializeController();
        pauseMenuUI.SetActive(false);
        settingsMenuUI.SetActive(false);

        menuButton.action.started += TogglePause;   
    }

    // Function to toggle pause
    public void TogglePause(InputAction.CallbackContext obj)
    {
        if (isPaused)
            ResumeGame();
        else
            PauseGame();
    }

    // Function to pause the game to show pause menu and freeze time
    public void PauseGame()
    {
        playerMovement.SetActive(false);
        pauseMenuUI.SetActive(true);
        settingsMenuUI.SetActive(false);

        Time.timeScale = 0f;
        isPaused = true;
    }

    // Function to resume the game and unfreeze time
    public void ResumeGame()
    {
        playerMovement.SetActive(true);
        pauseMenuUI.SetActive(false);
        settingsMenuUI.SetActive(false);

        Time.timeScale = 1f;
        isPaused = false;
    }

    // Called by the Resume button in the pause menu
    public void ResumeButton()
    {
        ResumeGame();
    }

    // Called by the Settings button in the pause menu
    public void SettingsButton()
    {
        pauseMenuUI.SetActive(false);
        settingsMenuUI.SetActive(true);
    }

    // Called by the Quit button in the pause menu
    public void QuitButton()
    {
        // Reset the state of game before quit
        Time.timeScale = 1f;
        PlayerPrefs.SetInt("MainScene_DialogueCompleted", 0);
        PlayerPrefs.Save();
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

}
