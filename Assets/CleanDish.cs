using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrip : MonoBehaviour
{
    [SerializeField] private Material[] stainMaterials;
    [SerializeField] private int cleaningSteps = 3;
    private int currentStep = 0;
    private Renderer stainRenderer;
    public ParticleSystem Bubble;

    // Start is called before the first frame update
    void Start()
    {
        stainRenderer = GetComponent<Renderer>();
        if (stainMaterials.Length > 0)
            stainRenderer.material = stainMaterials[0];
        else
            Debug.LogError("Stain materials not assigned!");
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag.Equals("Soap"))
        {
            currentStep++;

            if (currentStep < cleaningSteps && currentStep < stainMaterials.Length)
            {
                stainRenderer.material = stainMaterials[currentStep];
                //GameProgressManager.Instance.CleanStain();
            }
            else if (currentStep >= cleaningSteps)
            {
                currentStep = 0;
                Destroy(gameObject);
                
                // **清理完成，通知 GameProgressManager**
                GameProgressManager.Instance.CleanStain();
            }
        }
    }
}
