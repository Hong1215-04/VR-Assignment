using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ui_manager : MonoBehaviour
{

    [SerializeField] private GameObject titleScreenCanvas;
    [SerializeField] private GameObject settingScreenCanvas;
    [SerializeField] private GameObject creditScreenCanvas;

    // Show Setting Screen
    public void ShowSettingScreen()
    {
        titleScreenCanvas.SetActive(false);
        creditScreenCanvas.SetActive(false);
        settingScreenCanvas.SetActive(true);
        
    }

    // Show Title Screen
    public void ShowTitleScreen()
    {
        settingScreenCanvas.SetActive(false);
        creditScreenCanvas.SetActive(false);
        titleScreenCanvas.SetActive(true);

    }

    // Show Credit Screen
    public void ShowCreditScreen()
    {
        settingScreenCanvas.SetActive(false);
        titleScreenCanvas.SetActive(false);
        creditScreenCanvas.SetActive(true);

    }

}
