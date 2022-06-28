using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceBar : MonoBehaviour
{
    public Slider fill;

    public resource resourceType;
    public Dictionary<resource, Color> resourceColor = new Dictionary<resource, Color>()
    {
        {resource.Life, Color.red},
        {resource.Mana, Color.blue},
        {resource.Rage, new Color(1.0f, 0.4f, 0.0f)},
        {resource.Ammo, Color.yellow}
    };

    public void SetType(resource type)
    {
        resourceType = type;
        fill.GetComponent<Image>().color = resourceColor[resourceType];
    }

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
