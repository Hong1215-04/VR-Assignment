using System.Collections;
using UnityEngine;

public class DetactTrash : MonoBehaviour
{
    private GameObject trash;

    private void OnTriggerEnter(Collider collider)
    {
        List<GameObject> trashes = new List<GameObject>();
        trashes.Add(collider.gameObject);

        foreach (GameObject trash in trashes)
        {
            Debug.Log("list" + trash.name);
            if (trash.CompareTag("BinBag"))
            {
                Debug.Log(trash.name);
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
