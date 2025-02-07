using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class DetactTrash : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        List<GameObject> trashes = new List<GameObject>();
        trashes.Add(collider.gameObject);

        foreach (GameObject trash in trashes)
        {
            Debug.Log("list" + trash.name);
            if (trash.CompareTag("BinBag"))
            {
                collider.gameObject.GetComponent<XRGrabInteractable>().enabled = false;
                GameProgressManager.Instance.CleanTrash();
                StartCoroutine(destroyTrash(trash));
            }
        }
    }

    private IEnumerator destroyTrash(GameObject trash)
    {
        yield return new WaitForSeconds(3);

        Destroy(trash);
    }
}
