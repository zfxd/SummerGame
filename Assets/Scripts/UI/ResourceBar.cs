using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResourceBar : MonoBehaviour
{
    public Slider slider;
    public Image fill;
    public ResourceBarTextField currentText;
    public ResourceBarTextField dividerText;
    public ResourceBarTextField maxText;


    public void Init(resource type, float max, float current)
    {
        fill.color = UITheme.resourceColor[type];
        slider.maxValue = max;
        slider.value = current;
        UpdateText();
    }

    public void SetType(resource type)
    {
        fill.color = UITheme.resourceColor[type];
        UpdateText();
    }

    public void SetMax(float max)
    {
        slider.maxValue = max;
        UpdateText();
    }

    public void SetFill(float current)
    {
        slider.value = current;
        UpdateText();
    }

    public void UpdateText()
    {
        currentText.text.text = (slider.value).ToString();
        maxText.text.text = (slider.maxValue).ToString();
    }
}
