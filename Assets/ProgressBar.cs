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
    public Image outline;
    public GameObject canvas;
    public GameObject trash;
    public Transform trash_spawnPoint;

    // Update is called once per frame
    void Update()
    {
        GetCurrentFill();
    }

    void GetCurrentFill()
    {
        float fillAmount = (float)current / (float)max;
        mask.fillAmount = fillAmount;

        if (fillAmount == 1)
        {
            fill.color = Color.red;
            outline.color = Color.red;
        }
        else if (fillAmount >= 0.5)
        {
            fill.color = Color.yellow;
            outline.color = Color.yellow;
        }
        else if (fillAmount > 0)
        {
            fill.color = Color.green;
            outline.color = Color.green;
        }
        else
        {
            outline.color = Color.white;
        }
    }


    public void ResetCurrent()
    {
        if (current == max)
        {
            Instantiate(trash, trash_spawnPoint.position, Quaternion.identity);
            current = 0;
            fill.color = Color.white;
            outline.color = Color.white;
        }
    }
}
