using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class SetRigidBodyCenterOfMass : MonoBehaviour
{
    [SerializeField] private XRGrabInteractable XRGrabInteractable;
    [SerializeField] private new Rigidbody rigidbody;
    [SerializeField] private Vector3 newCenterOfMass;

    // Start is called before the first frame update
    void Start()
    {
        OnValidate();
        XRGrabInteractable.selectEntered.AddListener((X) => rigidbody.centerOfMass = newCenterOfMass);
        XRGrabInteractable.selectExited.AddListener((X) => rigidbody.ResetCenterOfMass());
    }

    private void OnValidate()
    {
        if (!XRGrabInteractable)
        {
            XRGrabInteractable = GetComponent<XRGrabInteractable>();
        }
        if (!rigidbody)
        {
            rigidbody = GetComponent<Rigidbody>();  
        }

    }
}
