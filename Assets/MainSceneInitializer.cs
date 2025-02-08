using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneInitializer : MonoBehaviour
{
    public GameObject player; 

    void Start()
    {
        // Load player last position in Main Scene if dialogue completed before
        if(PlayerPrefs.GetInt("MainScene_DialogueCompleted") == 1)
        {
            LoadState();
        }
        
    }

    // To safe the position of player
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

    }
}
