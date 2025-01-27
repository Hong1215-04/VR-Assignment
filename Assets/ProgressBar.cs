using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class ProgressBar : MonoBehaviour
{
    public int max;
    public int current = 0;
    public Image mask;
    public Image fill;
    public GameObject canvas;

    // Update is called once per frame
    void Update()
    {
        GetCurrentFill();
        if (current == max)
        {
            Debug.Log("Dustbin Full!!");
            fill.color = Color.red;
            canvas.SetActive(true);
        }
    }

    void GetCurrentFill()
    {
        float fillAmount = (float)current / (float)max;
        mask.fillAmount = fillAmount;
    }

    public void ResetCurrent()
    {
        current = 0;
        fill.color = Color.white;
    }
}
