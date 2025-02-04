using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ui_manager : MonoBehaviour
{

    [SerializeField] private GameObject titleScreenCanvas;
    [SerializeField] private GameObject settingScreenCanvas;
    [SerializeField] private GameObject inGameMainMenu;
    [SerializeField] private GameObject inGameSetting;

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


    public void ShowInGameMainMenu()
    {
        inGameSetting.SetActive(false);
        inGameMainMenu.SetActive(true);
    }

    public void ShowInGameSetting()
    {
        inGameMainMenu.SetActive(false);
        inGameSetting.SetActive(true);
    }
}
