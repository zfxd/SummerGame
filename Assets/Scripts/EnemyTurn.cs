using System.Collections;
using UnityEngine;

namespace Combat
{
    public class EnemyTurn : State
    {
        Unit Enemy;

        public EnemyTurn(BattleManager battleManager) : base(battleManager)
        {
        }

        public override IEnumerator Start()
        {
            Debug.Log("EnemyTurn");
            // Do stuff

            // Once done, return to TurnManager
            yield return new WaitForSeconds(2f);
            BattleManager.SetState(new TurnManager(BattleManager));
        }
    }
}