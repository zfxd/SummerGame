using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitBars : MonoBehaviour
{
    public Unit ownedBy;

    public Portrait unitPortrait;
    public Sprite unitPortraitSprite;

    public ResourceBar unitLifeBar;
    public Color unitLifeBarColor;
    public ResourceBar unitResourceBar;
    public Color unitResourceBarColor;
    public Dictionary<resource, Color> resourceColor = new Dictionary<resource, Color>()
        {
            {resource.Life, Color.red},
            {resource.Mana, Color.blue},
            {resource.Rage, new Color(1.0f, 0.4f, 0.0f)}, // Orange
            {resource.Ammo, Color.yellow}
        };

    void Start()
    {
        Init();
    }

    void Init()
    {
        //unitPortrait.SetPortrait(ownedBy.unitPortrait);

        unitLifeBarColor = Color.red;
        unitLifeBar.SetBarColor(unitLifeBarColor);

        unitResourceBarColor = resourceColor[ownedBy.unitResourceType];
        unitResourceBar.SetBarColor(unitResourceBarColor);

        UpdateUnitBars();
    }

    public void ChangeOwner(Unit newOwner) 
    {
        ownedBy = newOwner;
        Init();
    }

    public void UpdateUnitBars()
    {
        unitLifeBar.SetMax(ownedBy.unitLifeMax.value);
        unitLifeBar.SetFill(ownedBy.unitLife);

        unitResourceBar.SetMax(ownedBy.unitResourceMax.value);
        unitResourceBar.SetFill(ownedBy.unitResource);
    }
}