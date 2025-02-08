using UnityEngine;

public class Stain : MonoBehaviour
{
    [SerializeField] private Material[] stainMaterials; // Array of materials in different stage
    [SerializeField] private int cleaningSteps = 3; 
    private int currentStep = 0; 
    private Renderer stainRenderer; // Reference to object to change the material

    void Start()
    {
        stainRenderer = GetComponent<Renderer>();
        // Check if at least has one material
        if (stainMaterials.Length > 0)
            stainRenderer.material = stainMaterials[0];
        else
            Debug.LogError("Stain materials not assigned!");
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag.Equals("CleaningTool"))
        {
            currentStep++;

            if (currentStep < cleaningSteps && currentStep < stainMaterials.Length)
            {
                stainRenderer.material = stainMaterials[currentStep];
            }
            else if (currentStep >= cleaningSteps)
            {
                currentStep = 0; 
                Destroy(gameObject); // Remove stain

                // Pass to GameProgressManager when stain cleaned
                GameProgressManager.Instance.CleanStain();
            }
        }
    }
}
