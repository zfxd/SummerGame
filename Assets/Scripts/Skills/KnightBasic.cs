using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;

/**
 * Strike a single target twice for 2*50% damage.
 */
public class KnightBasic : Skill
{
    public override IEnumerator Activate(List<BattleTile> targets)
    {
        Unit target = targets[0].occupiedBy;
        bool killed = target.TakeDamage((int)(self.unitAtk.value * 0.5));
        if (killed)
            yield break; //End early if first hit kills!
        target.TakeDamage((int)(self.unitAtk.value * 0.5));
        yield break;
    }

    public override bool isValid(List<BattleTile> targeted)
    {
        return Validate.CheckForEnemy(targeted);
    }
}
