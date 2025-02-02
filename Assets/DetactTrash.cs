using System.Collections;
using UnityEngine;

public class DetactTrash : MonoBehaviour
{
    private GameObject trash;

    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log(collider.tag);
        if (collider.CompareTag("BinBag"))
        {
            Debug.Log("in");
            trash = collider.gameObject;
            GameProgressManager.Instance.CleanTrash();
            StartCoroutine (destroyTrash());
        }
    }

    private IEnumerator destroyTrash()
    {
        yield return new WaitForSeconds(3);

        Destroy(trash);
    }
}
