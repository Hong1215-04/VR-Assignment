using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetactTrash_SmallBin : MonoBehaviour
{
    public ProgressBar progressbar;

    private Rigidbody body;

    private void Update()
    {
        if (progressbar.current < progressbar.max)
        {
            GetComponent<Collider>().isTrigger = true;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        List<GameObject> trashes = new List<GameObject>();
        trashes.Add(collider.gameObject);

        foreach (GameObject trash in trashes)
        {
            Debug.Log("list" + trash.name);
            if (trash.CompareTag("Trash"))
            {
                Debug.Log(trash.name);
                progressbar.current = progressbar.current + 1;
                StartCoroutine(destroyTrash(trash));
            }
        }
    }

    private IEnumerator destroyTrash(GameObject trash)
    {
        yield return new WaitForSeconds(1);

        Debug.Log(trash.name);
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
