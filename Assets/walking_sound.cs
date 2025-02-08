using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class walking_sound : MonoBehaviour
{

    public InputActionReference locomotionButton;
    public GameObject isCanWalk;
    public AudioSource walkSound;

    private void Update()
    {
        // Detect the input or release of locomotion button
        locomotionButton.action.performed += LocomotionStay;
        locomotionButton.action.canceled += LocomotionCanceled;
    }


    // Play walk sound when walking
    private void LocomotionStay(InputAction.CallbackContext context)
    {
        if (isCanWalk.activeSelf)
        {
            if (!walkSound.isPlaying)
            {
                walkSound.Play();
            }
        }
    }

    // Stop walk sound when stop walking
    private void LocomotionCanceled(InputAction.CallbackContext context)
    {
        if (walkSound.isPlaying)
        {
            walkSound.Stop();

        }
    }

}
