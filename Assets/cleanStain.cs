using UnityEngine;

public class Stain : MonoBehaviour
{
    [SerializeField] private Material[] stainMaterials;
    [SerializeField] private int cleaningSteps = 3;
    private int currentStep = 0;
    private Renderer stainRenderer;

    void Start()
    {
        stainRenderer = GetComponent<Renderer>();
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
                //GameProgressManager.Instance.CleanStain();
            }
            else if (currentStep >= cleaningSteps)
            {
                // **清理完成，通知 GameProgressManager**
                GameProgressManager.Instance.CleanStain();

                Destroy(gameObject);
            }
        }
    }
}
