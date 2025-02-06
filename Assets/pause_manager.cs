using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject settingsMenuUI;
    public InputActionReference menuButton;

    private bool isPaused = false;

    void Start()
    {
        //InitializeController();
        pauseMenuUI.SetActive(false);
        settingsMenuUI.SetActive(false);

        menuButton.action.started += TogglePause;   
    }

    void Update()
    {

    }

    // Toggle pause state
    public void TogglePause(InputAction.CallbackContext obj)
    {
        if (isPaused)
            ResumeGame();
        else
            PauseGame();
    }

    // Pause the game: show the pause menu and freeze time
    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        settingsMenuUI.SetActive(false);

        Time.timeScale = 0f;
        isPaused = true;
    }

    // Resume the game: hide menus and unfreeze time
    public void ResumeGame()
    {
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
        // Before quitting, resume time scale to avoid freezing in the editor.
        Time.timeScale = 1f;
        PlayerPrefs.SetInt("MainScene_DialogueCompleted", 0);
        PlayerPrefs.Save();
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

}
