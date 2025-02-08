using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ui_overlay : MonoBehaviour
{

    [SerializeField] private Transform vrCamera; // Assign the VR camera in the Inspector
    [SerializeField] private float distanceFromCamera = 1.5f;
    [SerializeField] private Vector3 offset = new Vector3(0, 0.5f, 0);


    // Update is called once per frame
    private void Update()
    {
        //Move the UI in front of camera make it like overlay UI
        Vector3 targetPosition = vrCamera.position + vrCamera.forward * distanceFromCamera + offset;
        transform.position = targetPosition;
        transform.rotation = Quaternion.LookRotation(transform.position - vrCamera.position);
    }
}
