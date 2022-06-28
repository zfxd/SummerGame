using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartyStatusDisplay : MonoBehaviour
{
    public UnitBars unit1;
    public UnitBars unit2;
    public UnitBars unit3;
    public UnitBars unit4;

    public UnitBars[] unitBars = new UnitBars[4];

    void Start()
    {
        unitBars[0] = unit1;
        unitBars[1] = unit2;
        unitBars[2] = unit3;
        unitBars[3] = unit4;
    }
}