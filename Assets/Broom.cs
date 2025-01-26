using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Broom : MonoBehaviour
{
    [SerializeField] private AudioSource cleaningAudio; // Reference to the Audio Source

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Stain"))
        {
            PlayCleaningSound();
        }
    }

    // Play cleaning sound effectg
    void PlayCleaningSound()
    {
        cleaningAudio.pitch = Random.Range(0.8f, 1.3f); // Rondomize the pitch to make it sound slightly different in each repetition
        cleaningAudio.Play();
    }
}
