using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetactTrash_SmallBin : MonoBehaviour
{
    [SerializeField]
    private ProgressBar progressbar;
    private GameObject trash;

    private void Start()
    {
        
    }

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
        yield return new WaitForSeconds(3);

        Destroy(trash);
    }
}
