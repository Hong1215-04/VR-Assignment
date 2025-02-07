using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill_CockRoach : MonoBehaviour
{
    private int cleaningSteps = 1;
    private int currentStep = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag.Equals("CleaningTool"))
        {
            currentStep++;

            if (currentStep >= cleaningSteps)
            {
                currentStep = 0;
                Debug.Log(gameObject.name);
                Destroy(gameObject);

                // **清理完成，通知 GameProgressManager**
                GameProgressManager.Instance.CleanCockroach();
            }
        }
    }
}
