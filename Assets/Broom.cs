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
        if (other.tag.Equals("Stain"))
        {
            PlayCleaningSound();
        }
        else if (other.tag.Equals("Cockroach"))
        {
            killCockroachAudio.Play();
        }
    }

    // Play cleaning sound effectg
    void PlayCleaningSound()
    {
        cleaningAudio.pitch = Random.Range(0.8f, 1.3f); // Rondomize the pitch to make it sound slightly different in each repetition
        cleaningAudio.Play();
    }

}
