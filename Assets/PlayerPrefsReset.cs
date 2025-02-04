using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsReset : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        // Clear specific key to reset dialogue for main scene
        PlayerPrefs.SetInt("MainScene_DialogueCompleted", 0);
        PlayerPrefs.Save();
    }

}
