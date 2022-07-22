using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;

public class Skill : ScriptableObject
{
    int cooldownTime;
    [SerializeField] public Unit self;
    TargetMode targetMode;

    public virtual IEnumerator Activate(List<BattleTile> targets)
    {
        yield break;
    }

    public virtual bool isValid(List<BattleTile> targeted)
    {
        Debug.Log("YOU SHOULD NEVER SEE THIS. SKILL.VALIDATE RETURNS FALSE.");
        return false;
    }
}
