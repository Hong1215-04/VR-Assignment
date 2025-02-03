using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestrictPlayerMovement : MonoBehaviour
{
    public GameObject playerMovement;
    // Start is called before the first frame update
    void Start()
    {
        playerMovement.SetActive(false);
    }

}
