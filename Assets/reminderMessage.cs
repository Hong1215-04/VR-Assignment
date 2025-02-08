using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEditor;
using UnityEngine.XR.Interaction.Toolkit.Locomotion;
using Unity.XR.CoreUtils;

public class reminderMessage : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    public AudioSource dialogueSFX;

    private int index;

    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
        Invoke("StartDialogue", 1.5f);
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        dialogueSFX.Play();
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        dialogueSFX.Stop();
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);

            // Set the dialogue completion flag in PlayerPrefs.
            PlayerPrefs.SetInt("MainScene_DialogueCompleted", 1);
            PlayerPrefs.Save();

        }
    }

    public void OnActivated()
    {
        if (textComponent.text == lines[index])
        {
            NextLine();
        }
        else
        {
            StopAllCoroutines();
            textComponent.text = lines[index];
        }
    }
}
