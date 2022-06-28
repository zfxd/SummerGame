using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
    public PartyStatusDisplay partyStatusDisplay;

    public void Init(List<Unit> allyUnits, List<Unit> enemyUnits)
    {
        partyStatusDisplay.Init(allyUnits, enemyUnits);
    }
}