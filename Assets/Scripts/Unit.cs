using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;
    public int unitLevel;
    public enum affl {ALLY, ENEMY}

    public int atk;
    public int spd;

    public int maxHP;
    public int currHP;

    public void TakeDamage(int dmg)
    {
        currHP -= dmg;
        Debug.Log(unitName + " took " + dmg + " Damage");
    }
}
