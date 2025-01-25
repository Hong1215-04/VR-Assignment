using UnityEngine;

public class Stain : MonoBehaviour
{
    [SerializeField] private Material[] stainMaterials; // Array to store the materials (in different opacity)
    [SerializeField] private int cleaningSteps = 3; // Total cleaning steps to remove the stain
    private int currentStep = 0; // Tracks the current cleaning step

    private Renderer stainRenderer; // Reference to the Renderer component

    void Start()
    {
        // Get the Renderer component of the stain
        stainRenderer = GetComponent<Renderer>();

        // Ensure the initial material set to first material in the array
        if (stainMaterials.Length > 0)
        {
            stainRenderer.material = stainMaterials[0];
        }
        else
        {
            Debug.LogError("Stain materials not assigned!");
        }
    }

    // When interact with stain
    void OnTriggerEnter(Collider other)
    {
        // Check if the interacting object is the cleaning tool
        if (other.CompareTag("CleaningTool"))
        {
            // Increment the cleaning step
            currentStep++;

            // Update the stain material based on the current step
            if (currentStep < cleaningSteps && currentStep < stainMaterials.Length)
            {
                stainRenderer.material = stainMaterials[currentStep];
            }
            else if (currentStep >= cleaningSteps)
            {
                // Destroy the stain once cleaning is complete
                Destroy(gameObject);
            }
        }
    }
}
