using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class DetactTrash_SmallBin : MonoBehaviour
{
    public ProgressBar progressbar;

    private GameObject trash;
    private Rigidbody body;


    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Trash"))
        {
            trash = collider.gameObject;
            progressbar.current = progressbar.current + 1;
            StartCoroutine(destroyTrash());
        }

    }

    private IEnumerator destroyTrash()
    {
        yield return new WaitForSeconds(2);

        Destroy(trash);

        if (progressbar.current >= progressbar.max)
        {
            Debug.Log("Is Full");
            GetComponent<Collider>().isTrigger = false;
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
