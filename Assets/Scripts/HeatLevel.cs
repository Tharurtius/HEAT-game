using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatLevel : MonoBehaviour
{
    public int heatLevel;
    public GameObject[] heatLevelImage;

    public void HeatLevelUp()
    {
        //spawn object when heat level goes up
        heatLevel++;
        if (heatLevel == 1)
        {
            heatLevelImage[0].SetActive(true);
        }
        if (heatLevel == 2)
        {
            heatLevelImage[1].SetActive(true);
        }
        if (heatLevel == 3)
        {
            heatLevelImage[2].SetActive(true);
        }
        if (heatLevel == 4)
        {
            heatLevelImage[3].SetActive(true);
        }
        if (heatLevel == 5)
        {
            heatLevelImage[4].SetActive(true);
        }

        // Diagnostic check
        Debug.Log("Heat level up!");
    }

    public void HeatLevelDown()
    {

    }
}
