using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ui_manager : MonoBehaviour
{

    [SerializeField] private GameObject titleScreenCanvas;
    [SerializeField] private GameObject settingScreenCanvas;
    [SerializeField] private scene_manager sm;

    public void ShowSettingScreen()
    {
        titleScreenCanvas.SetActive(false);
        settingScreenCanvas.SetActive(true);
    }

    public void ShowTitleScreen()
    {
        settingScreenCanvas.SetActive(false);
        titleScreenCanvas.SetActive(true);
    }

}
