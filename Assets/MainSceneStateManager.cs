using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneStateManager : MonoBehaviour
{
    public GameObject player; // Reference to the player GameObject in MainScene.
    public bool isDialogueCompleted; // Flag for dialogue completion.
    public bool firstTime = true;

    // Call this method before leaving MainScene (e.g., when pressing a button to load Scene2)
    public void SaveState()
    {
        // Save player's position.
        Vector3 pos = player.transform.position;
        PlayerPrefs.SetFloat("MainScene_PlayerPosX", pos.x);
        PlayerPrefs.SetFloat("MainScene_PlayerPosY", pos.y);
        PlayerPrefs.SetFloat("MainScene_PlayerPosZ", pos.z);

        // Save dialogue completion state.
        PlayerPrefs.SetInt("MainScene_DialogueCompleted", isDialogueCompleted ? 1 : 0);

        // Save immediately to disk.
        PlayerPrefs.Save();
    }
}
