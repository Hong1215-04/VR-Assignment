using System.Collections;
using System.Diagnostics;
using TMPro;
using UnityEngine;

public class DetactTrash : MonoBehaviour
{
    public TMP_Text txt_task;

    private GameObject trash;
    private int task = 15;

    private void Start()
    {
        txt_task.SetText("Task Remaining: " + task.ToString());
    }

    private void OnTriggerEnter(Collider collider)
    {
        UnityEngine.Debug.Log(collider.tag);
        if (collider.CompareTag("Trash"))
        {
            UnityEngine.Debug.Log("yes");
            trash = collider.gameObject;
            task = task - 1;
            txt_task.SetText("Task Remaining: " + task.ToString());
            StartCoroutine (destroyTrash());
        }
    }

    private IEnumerator destroyTrash()
    {
        yield return new WaitForSeconds(3);

        Destroy(trash);
    }
}
