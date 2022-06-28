using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceBar : MonoBehaviour
{
    public Slider slider;
    public Image fill;

    public void SetType(resource type)
    {
        fill.color = UITheme.resourceColor[type];
    }

    public void SetMax(float max)
    {
        slider.maxValue = max;
        slider.value = max;
    }

    public void SetFill(float resourceLevel)
    {
        slider.value = resourceLevel;
    }
}
