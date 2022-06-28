using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartyStatusDisplay : MonoBehaviour
{
    public UnitBars[] allyUnitBars = new UnitBars[4];
    public UnitBars[] enemyUnitBars = new UnitBars[9];

    public void Init(List<Unit> allyUnits, List<Unit> enemyUnits)
    {
        for (int i = 0; i < allyUnits.Count; i++)
        {
            allyUnitBars[i].ChangeOwner(allyUnits[i]);
            allyUnitBars[i].Enable();
        }

        for (int i = 0; i < enemyUnits.Count; i++)
        {
            enemyUnitBars[i].ChangeOwner(enemyUnits[i]);
            enemyUnitBars[i].Enable();
        }
    }
}