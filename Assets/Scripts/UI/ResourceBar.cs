using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceBar : MonoBehaviour
{
    public Slider slider;
    public Image fill;

    public void SetBarColor(Color newColor)
    {
        fill.color = newColor;
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
