using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class DetactTrash_SmallBin : MonoBehaviour
{
    public ProgressBar progressbar;
    public AudioSource collectSFX;

    private Rigidbody body;


    private void OnTriggerEnter(Collider collider)
    {
        List<GameObject> trashes = new List<GameObject>();
        trashes.Add(collider.gameObject);

        foreach (GameObject trash in trashes)
        {
            Debug.Log("list" + trash.name);
            if (trash.CompareTag("Trash"))
            {
                if (collider.gameObject.GetComponent<XRGrabInteractable>() != null)
                {
                    collider.gameObject.GetComponent<XRGrabInteractable>().enabled = false;
                }
                Destroy(trash);
                collectSFX.Play();
                if (progressbar.current >= progressbar.max)
                {
                    Debug.Log("Is Full");
                    GetComponent<Collider>().isTrigger = false;
                }
                else
                {
                    progressbar.current = progressbar.current + 1;
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision" + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Trash"))
        {
            body = collision.rigidbody;
            body.AddForce(new Vector3(2, 3, 0), ForceMode.Impulse);

        }
    }
}
