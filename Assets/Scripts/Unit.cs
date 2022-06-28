using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum affl {ALLY, ENEMY};
public enum resource {Life, Mana, Rage, Ammo};

public class Unit : MonoBehaviour
{
    // UI
    public ResourceBar unitLifeBar;
    public ResourceBar unitResourceBar;

    // Characteristics
    public string   unitName;
    public int      unitLevel;
    public affl     unitAffl;

    // Damage Calculation Stats
    public UnitStat unitAtk;
    public UnitStat unitDef;

    // Utility Stats
    public UnitStat unitSpd;

    // Resources
    public UnitStat     unitLifeMax;
    public float        unitLife;
    public resource     unitResourceType;
    public UnitStat     unitResourceMax;
    public float        unitResource;
    public float        unitArmor;
    public float        unitShield;
    
    // Constructor
    public Unit()
    {
        unitAffl = affl.ALLY;
        unitAtk = new UnitStat(100);
        unitDef = new UnitStat(100);
        unitSpd = new UnitStat(100);
        unitLifeMax = new UnitStat(100);
        unitLife = unitLifeMax.value;
        unitResourceType = resource.Mana;
        unitResourceMax = new UnitStat(100);
        unitResource = unitResourceMax.value;
        unitArmor = 0;
        unitShield = 0;
    }

    // Flat Damage or final result of %-based Damage
    public void TakeDamage(int dmg)
    {
        unitLife -= dmg;
        Debug.Log(unitName + " took " + dmg + " Damage");
    }
}
