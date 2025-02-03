using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgm_manager : MonoBehaviour
{

    private AudioSource audioSource;
    [SerializeField] private AudioClip[] clips; // Array to store BGM list

    // Start is called before the first frame update
    void Start()
    {
        // Get audio source component
        audioSource = GetComponent<AudioSource>();

        // Play BGM when start
        PlayBGM(0);

    }

    // Function to play BGM
    public void PlayBGM(int i)
    {
        audioSource.clip = clips[i];
        audioSource.Play();
    }

    // Function to stop BGM
    public void StopBGM()
    {
        audioSource.Stop();
    }


}
