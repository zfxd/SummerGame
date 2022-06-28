using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitBars : MonoBehaviour
{
    public Unit ownedBy;
    public ResourceBar unitLifeBar;
    public ResourceBar unitResourceBar;

    void Start()
    {
        Init();
    }

    void Init()
    {
        unitLifeBar.SetType(resource.Life);
        unitResourceBar.SetType(ownedBy.unitResourceType);
        UpdateUnitBars();
    }

    void ChangeOwner(Unit newOwner)
    {
        ownedBy = newOwner;
        Init();
    }

    void UpdateUnitBars()
    {
        unitLifeBar.SetMax(ownedBy.unitLifeMax.value);
        unitLifeBar.SetFill(ownedBy.unitLife);

        unitResourceBar.SetMax(ownedBy.unitResourceMax.value);
        unitResourceBar.SetFill(ownedBy.unitResource);
    }


}