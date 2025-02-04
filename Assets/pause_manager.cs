using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject settingsMenuUI;
    public InputDevice InputDevice;


    private bool isPaused = false;
    // This flag prevents multiple toggles when the menu button is held down
    private bool menuButtonPressedLastFrame = false;

    void Start()
    {
        //InitializeController();
        pauseMenuUI.SetActive(false);
        settingsMenuUI.SetActive(false);
    }

    void Update()
    {
        //// Get the left hand device (Oculus menu button is on the left controller)
        //List<InputDevice> leftHandDevices = new List<InputDevice>();
        //InputDevices.GetDevicesAtXRNode(XRNode.LeftHand, leftHandDevices);

        //if (leftHandDevices.Count > 0)
        //{
            //InputDevice leftDevice = leftHandDevices[0];
            bool menuButtonPressed = false;
            if (InputDevice.TryGetFeatureValue(CommonUsages.menuButton, out menuButtonPressed) && menuButtonPressed)
            {
                // Only toggle when the button is first pressed
                if (!menuButtonPressedLastFrame)
                {
                    TogglePause();
                }
                menuButtonPressedLastFrame = true;
            }
            else
            {
                menuButtonPressedLastFrame = false;
            }
        //}
    }

    // Toggle pause state
    public void TogglePause()
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

    void InitializeController()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDeviceCharacteristics controllerCharacteristics =
            InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Right;

        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);

        if (devices.Count > 0)
        {
            InputDevice = devices[0];
            Debug.Log("Controller Found: " + InputDevice.name);
        }
    }
}
