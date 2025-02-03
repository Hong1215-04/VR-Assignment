using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneInitializer : MonoBehaviour
{
    public GameObject player; 

    public bool isDialogueCompleted;

    public MainSceneStateManager msManager;

    private void Awake()
    {
        if (msManager.firstTime)
        {
            PlayerPrefs.SetInt("MainScene_DialogueCompleted", 0);
            PlayerPrefs.Save();
            msManager.firstTime = false;
        }
        
    }

    void Start()
    {
        LoadState();
    }

    void LoadState()
    {
        // Check if the player position was saved.
        if (PlayerPrefs.HasKey("MainScene_PlayerPosX"))
        {
            float x = PlayerPrefs.GetFloat("MainScene_PlayerPosX");
            float y = PlayerPrefs.GetFloat("MainScene_PlayerPosY");
            float z = PlayerPrefs.GetFloat("MainScene_PlayerPosZ");
            player.transform.position = new Vector3(x, y, z);
        }

        // Restore the dialogue completion state.
        if (PlayerPrefs.HasKey("MainScene_DialogueCompleted"))
        {
            isDialogueCompleted = PlayerPrefs.GetInt("MainScene_DialogueCompleted") == 1;
        }
    }
}
