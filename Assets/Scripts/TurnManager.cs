using System.Collections;
using UnityEngine;

namespace Combat
{
    public class TurnManager : State
    {
        public TurnManager(BattleManager battleManager) : base(battleManager)
        {
        }

        public override IEnumerator Start()
        {
            Debug.Log("TurnManager begin");
            // Check for win/losecon
            
            // Check if anyone is above threshold
            while(BattleManager.turnOrder.GetPriority(BattleManager.turnOrder.First) < 1000)
            {
                // If no one, increment until someone is
                foreach(Unit unit in BattleManager.turnOrder)
                {
                    BattleManager.turnOrder.UpdatePriority(unit, BattleManager.turnOrder.GetPriority(unit) + unit.unitSpd.value);
                }
            }

            Unit currUnit = BattleManager.turnOrder.First;
            // First move back to bottom of turnorder
            BattleManager.turnOrder.UpdatePriority(currUnit, 0);
            // Give them the turn
            if (currUnit.unitAffl == affl.ALLY){
                BattleManager.takingTurn = currUnit;
                BattleManager.SetState(new PlayerTurn(BattleManager));
            }
            else{
                BattleManager.takingTurn = currUnit;
                BattleManager.SetState(new EnemyTurn(BattleManager));
            }
            yield break;
        }
    }
}