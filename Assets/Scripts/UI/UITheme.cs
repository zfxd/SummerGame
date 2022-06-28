// Contains data for common UI needs like the color of different things.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class UITheme
{
    public static Dictionary<resource, Color> resourceColor = new Dictionary<resource, Color>()
        {
            {resource.None, Color.clear},
            {resource.Life, Color.red},
            {resource.Mana, Color.blue},
            {resource.Rage, new Color(1.0f, 0.4f, 0.0f)}, // Orange
            {resource.Ammo, Color.yellow}
        };
}