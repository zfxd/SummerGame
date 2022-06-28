using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceBar : MonoBehaviour
{
    public Slider fill;

    public void SetMax(float max)
    {
        fill.maxValue = max;
        fill.value = max;
    }

    public void SetFill(float resourceLevel)
    {
        fill.value = resourceLevel;
    }
}
