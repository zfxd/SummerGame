using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;

/**
 * Strike a single target twice for 2*50% damage.
 */
 [CreateAssetMenu]
public class KnightBasic : Skill
{
    public override void Awake()
    {
        this.skillName = "KnightBasic";
        this.targetMode = TargetMode.SINGLE;
    }
    
    public override IEnumerator SkillEffect(BattleManager BattleManager)
    {
        Unit target = BattleManager.targeted[0].occupiedBy;
        bool killed = target.TakeDamage((int)(BattleManager.takingTurn.unitAtk.value * 0.5));
        Debug.Log("KnightBasic hit #1 for " + BattleManager.takingTurn.unitAtk.value * 0.5 + " dmg");
        yield return new WaitForSeconds(2f);
        if (killed)
        {
            BattleManager.enemyUnits.Remove(target);
            BattleManager.turnOrder.Remove(target);
            BattleManager.busy = false;
            yield break; //End early if first hit kills!
        }
        killed = target.TakeDamage((int)(BattleManager.takingTurn.unitAtk.value * 0.5));
        Debug.Log("KnightBasic hit #2 for " + BattleManager.takingTurn.unitAtk.value * 0.5 + " dmg");
        if (killed)
        {
            BattleManager.enemyUnits.Remove(target);
            BattleManager.turnOrder.Remove(target);
        }
        BattleManager.busy = false;
        yield break;
    }

    public override bool IsValid(List<BattleTile> targeted)
    {
        return Validate.IsEnemy(targeted) && Validate.IsOccupied(targeted);
    }
}
