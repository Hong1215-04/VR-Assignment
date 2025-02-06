using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ui_manager : MonoBehaviour
{

    [SerializeField] private GameObject titleScreenCanvas;
    [SerializeField] private GameObject settingScreenCanvas;
    [SerializeField] private GameObject creditScreenCanvas;

    public void ShowSettingScreen()
    {
        titleScreenCanvas.SetActive(false);
        creditScreenCanvas.SetActive(false);
        settingScreenCanvas.SetActive(true);
        
    }

    public void ShowTitleScreen()
    {
        settingScreenCanvas.SetActive(false);
        creditScreenCanvas.SetActive(false);
        titleScreenCanvas.SetActive(true);

    }

    public void ShowCreditScreen()
    {
        settingScreenCanvas.SetActive(false);
        titleScreenCanvas.SetActive(false);
        creditScreenCanvas.SetActive(true);

    }

}
