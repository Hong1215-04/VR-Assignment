using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class broomDetact : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("CleaningTool"))
        {
            Debug.Log("broom");
            GetComponent<Rigidbody>().isKinematic = true;   
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("CleaningTool"))
        {
            Debug.Log("broomExit");
            GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
