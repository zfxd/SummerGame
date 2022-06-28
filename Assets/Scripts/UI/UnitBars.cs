using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitBars : MonoBehaviour
{
    public Unit ownedBy;
    public bool isEnabled;
    public Canvas canvas;

    public Portrait unitPortrait;

    public ResourceBar unitLifeBar;
    public ResourceBar unitResourceBar;
    
    void Start()
    {
        ownedBy = null;
        UpdateAll();
    }

    public void ChangeOwner(Unit newOwner) 
    {
        ownedBy = newOwner;
        UpdateAll();
    }

    public void Enable()
    {
        isEnabled = true;
        canvas.enabled = isEnabled;
        UpdateAll();
    }

    public void Disable()
    {
        enabled = false;
        canvas.enabled = isEnabled;
        ClearAll();
    }

    public void UpdateAll()
    {
        if (ownedBy == null)
        {
            Disable();
        }
        else
        {
            // UpdateUnitPortrait();
            UpdateUnitBars();
        }
    }

    public void ClearAll()
    {
        unitLifeBar.SetType(resource.None);
        unitResourceBar.SetType(resource.None);
    }

    public void UpdateUnitPortrait()
    {
        unitPortrait.SetPortrait(ownedBy.unitPortrait);
    }

    public void UpdateUnitBars()
    {
        unitLifeBar.SetType(resource.Life);
        unitLifeBar.SetMax(ownedBy.unitLifeMax.value);
        unitLifeBar.SetFill(ownedBy.unitLife);

        unitResourceBar.SetType(ownedBy.unitResourceType);
        unitResourceBar.SetMax(ownedBy.unitResourceMax.value);
        unitResourceBar.SetFill(ownedBy.unitResource);
    }
}