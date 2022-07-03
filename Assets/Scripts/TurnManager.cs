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
                    Debug.Log("No one yet");
                    BattleManager.turnOrder.UpdatePriority(unit, BattleManager.turnOrder.GetPriority(unit) + unit.unitSpd.value);
                }
            }

            Unit currUnit = BattleManager.turnOrder.First;
            // First move back to bottom of turnorder
            BattleManager.turnOrder.UpdatePriority(currUnit, 0);
            // Give them the turn
            yield return new WaitForSeconds(2f);
            if (currUnit.unitAffl == affl.ALLY){
                BattleManager.SetState(new PlayerTurn(BattleManager));
            }
            else{
                BattleManager.SetState(new EnemyTurn(BattleManager));
            }
        }
    }
}