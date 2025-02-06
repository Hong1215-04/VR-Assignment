using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoapDetect : MonoBehaviour
{
    [SerializeField] private AudioSource washingAudio;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Soap"))
        {
            Debug.Log("broom");
            GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Soap"))
        {
            Debug.Log("broomExit");
            washingAudio.pitch = Random.Range(0.8f, 1.3f);
            washingAudio.Play();
            GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
