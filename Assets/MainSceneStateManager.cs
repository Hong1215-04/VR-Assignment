using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using System.Globalization;

public class MainSceneStateManager : MonoBehaviour
{
    public GameObject player; // Reference to the player GameObject in MainScene.
    public bool isDialogueCompleted; // Flag for dialogue completion.
    private bool timerRunning = false;
    private float time = 60f;
    [SerializeField] private GameObject reminder;

    private void Update()
    {
        // Show a reminder to player if there did not know what to do after a time
        if (PlayerPrefs.GetInt("MainScene_DialogueCompleted", 0) == 1)
        {
            timerRunning = true;
        }

            if (timerRunning)
        {
            if (time < 0)
            {
                timerRunning = false;
                reminder.SetActive(true);
            }
            else
            {
                time -= Time.deltaTime;
            }
        }

    }

    // Function to save the last postion of player in Main Scene
    public void SaveState()
    {
        // Save player's position.
        Vector3 pos = player.transform.position;
        PlayerPrefs.SetFloat("MainScene_PlayerPosX", pos.x);
        PlayerPrefs.SetFloat("MainScene_PlayerPosY", pos.y);
        PlayerPrefs.SetFloat("MainScene_PlayerPosZ", pos.z);

        // Save data
        PlayerPrefs.Save();
    }

}
