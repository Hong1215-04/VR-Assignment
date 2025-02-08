using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Broom : MonoBehaviour
{
    [SerializeField] private AudioSource cleaningAudio; // Audio when sweep stain
    [SerializeField] private AudioSource killCockroachAudio; // Audio when kill cockroach

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Stain")) // If collide with stain
        {
            PlayCleaningSound();
        }
        else if (other.tag.Equals("Cockroach")) // If collide with cockroach
        {
            killCockroachAudio.Play();
        }
    }

    // Play cleaning sound effect
    void PlayCleaningSound()
    {
        cleaningAudio.pitch = Random.Range(0.8f, 1.3f); // Rondomize the pitch to make it sound slightly different in each repetition
        cleaningAudio.Play();
    }

}
