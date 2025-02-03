using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class audio_manager : MonoBehaviour
{

    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;

    // Start is called before the first frame update
    private void Start()
    {
        // Load player setting if have data before, if no, set default
        if(PlayerPrefs.HasKey("BGMVolume") || PlayerPrefs.HasKey("SFXVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetBGMVolume();
            SetSFXVolume();
        }

    }

    
    // Adjust BGM volume
    public void SetBGMVolume()
    {
        float volume = bgmSlider.value;
        audioMixer.SetFloat("BGMVolume", Mathf.Log10(volume) * 20); // Set the volume in decibels
        PlayerPrefs.SetFloat("BGMVolume", volume); // Save player bgm setting

    }

    // Adjust SFX volume
    public void SetSFXVolume()
    {
        float volume = sfxSlider.value;
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20); // Set the volume in decibels
        PlayerPrefs.SetFloat("SFXVolume", volume); // Save player sfx setting

    }

    // Load previous player setting
    private void LoadVolume()
    {
        bgmSlider.value = PlayerPrefs.GetFloat("BGMVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");

        SetBGMVolume();
        SetSFXVolume();
    }

}
